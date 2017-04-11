static HashSet<IMyInventory> invenLocked = new HashSet<IMyInventory>();
// static List<IMyInventoryItem> stacks;
static float maxCollectedSolarPanel = 120;
float previousEnergyBattery = 0;
DateTime previousTime; 

/** 
 * Apache License 2.0 
 * Written by lquattro (Scuriva Elquattro)
 */ 
public void Main(string argument) {

    // Show Clock Time 
    TimeSpan timeOffset = new TimeSpan(0, 0, 0, 0);       
    DateTime currentTime = DateTime.Now;       
    DateTime currentTimeActual = currentTime.Add(timeOffset);  
    string timeClock = currentTimeActual.ToString("HH:mm:ss");         

    /** 
     * Variables texts
     */ 
    string textClock = "                                    Clock: ";
    string textBattery = "\n---==Battery Energy==---";
    string textSolar = "\n---==Solar Energy==---";
    string textConso = "\n---==Conso Energy==---";
    string textReactor = "\n---==Reactor Energy==---";
    string textTest = "\n---==Test Energy==---";

    // LCD Panels
    List<IMyTextPanel> lcds = new List<IMyTextPanel>();    
    GridTerminalSystem.GetBlocksOfType<IMyTextPanel>(lcds);
    // Solar Panels
    List<IMySolarPanel> solarPanels = new List<IMySolarPanel>();   
    GridTerminalSystem.GetBlocksOfType<IMySolarPanel>(solarPanels);
    // Battery Batteries
    List<IMyBatteryBlock> batteries = new List<IMyBatteryBlock>();    
    GridTerminalSystem.GetBlocksOfType<IMyBatteryBlock>(batteries);
    // Battery Reactors
    List<IMyReactor> reactors = new List<IMyReactor>();     
    GridTerminalSystem.GetBlocksOfType<IMyReactor>(reactors);
    // All Block Thrust
    List<IMyThrust> allBlock = new List<IMyThrust>();
    GridTerminalSystem.GetBlocksOfType<IMyThrust>(allBlock);
    
    // Calculate Solars
    float maxPowerSolar = 0;
    float solars = 0;
    for (int i=0; i < solarPanels.Count; i++){ 
        solars += solarPanels[i].MaxOutput;
        if ( maxPowerSolar < solarPanels[i].MaxOutput ) {
            maxPowerSolar = solarPanels[i].MaxOutput;
        }
    } 
    // Calculate Batteries
    float batteri = 0;
    for (int i=0; i < batteries.Count; i++){ 
        batteri += batteries[i].CurrentStoredPower;
    } 
    // Calculate Batteries Conso
    float energyBattery = batteri;
    // DelayTime
    TimeSpan delay = currentTime - previousTime;
    // Calculate Thrust
    foreach  ( IMyThrust block in allBlock ) {
        textTest += "\n----\n" + block.CurrentThrust + "Newtons";
        textTest += "\n Max Pos" + block.Max;
        textTest += "\n Min Pos" + block.Min;
    }
    // Calculate Reactors Power & Quantity Uranium
    float reacts = 0;
    float uraAmount = 0;
    IMyInventory inven;
    IMyInventoryItem invenItem;
    foreach  ( IMyReactor reactor in reactors ) {
        //  Calculate Uranium in this Reactor
        inven = (IMyInventory) reactor.GetInventory(0);
        invenItem = (IMyInventoryItem) inven.GetItems()[0];
        uraAmount += (float) invenItem.Amount;
        // Calculate Reactor Power
        reacts += reactor.CurrentOutput;
    }
    
    // Write Text Clock
    textClock += timeClock;
    // Write Text Battery
    textBattery += "\nNumber: " + batteries.Count; 
    textBattery +=  "\nTotal kW: " + batteri * 1000; 
    // Write Text SolarPanel
    textSolar += "\nNumber: " + solarPanels.Count; 
    textSolar += "\nTotal kW: " + solars * 1000;
    textSolar += "\nPanel Average kW: " + ( solars * 1000 / solarPanels.Count );
    textSolar += "\nPanels Efficacity %: " + ( solars * 1000 / solarPanels.Count ) * 100 / maxCollectedSolarPanel;
    textSolar += "\nBest Panel kW: " + maxPowerSolar * 1000;
    // Write Text Conso
    textConso += "\nBattery Conso kW/" +  delay.Seconds + "s: " + ( energyBattery - previousEnergyBattery ) * 1000;
    // Write Text Reactor
    textReactor += "\nNumber: " + reactors.Count;
    textReactor += "\nTotal kW: " + reacts * 1000;
    textReactor += "\nTotal Nb Uranium: " + uraAmount;

    foreach ( IMyTextPanel lcd in lcds ) {
        // Exit if not contain tag mod SE
        if ( !lcd.CustomName.Contains("SE") ) {
            continue;
        }
        string text = "";
        if ( lcd.CustomName.Contains("CLOCK") ) { 
            text += textClock; 
        }
        if ( lcd.CustomName.Contains("SOLAR") ) {
            text += textSolar;
        }
        if ( lcd.CustomName.Contains("BATTERY") ) {
            text += textBattery;
        }
        if ( lcd.CustomName.Contains("CONSO") ) { 
            text += textConso;
        }
        if ( lcd.CustomName.Contains("REACTOR") ) {  
            text += textReactor; 
        }

        // Write test in displays
        lcd.WritePublicText(text, false);
        // Show public text in displays 
        lcd.ShowPublicTextOnScreen();
    }

    // End task
    previousEnergyBattery = energyBattery;
    previousTime = currentTime;
} 

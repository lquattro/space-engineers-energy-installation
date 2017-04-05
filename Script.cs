static HashSet<IMyInventory> invenLocked = new HashSet<IMyInventory>();
// static List<IMyInventoryItem> stacks;
static float maxCollectedSolarPanel = 120;

/** 
 * Apache License 2.0 
 * Written by lquattro (Scuriva Elquattro)
 */ 
public void Main(string argument) {
 
    /** 
     * Variables 
     */ 
    // Total kW Calculated for all SolarPanel  
    string textB = "Battery Energy\n----------";
    string textS = "Solar Energy\n----------";

    // LCD Panels
    List<IMyTextPanel> lcds = new List<IMyTextPanel>();    
    GridTerminalSystem.GetBlocksOfType<IMyTextPanel>(lcds);
    // Solar Panels
    List<IMySolarPanel> solarPanels = new List<IMySolarPanel>();   
    GridTerminalSystem.GetBlocksOfType<IMySolarPanel>(solarPanels);
    // Battery Blocks
    List<IMyBatteryBlock> batteries = new List<IMyBatteryBlock>();    
    GridTerminalSystem.GetBlocksOfType<IMyBatteryBlock>(batteries);
    
    float maxPowerSolar = 0;
    float solars = 0;
    for (int i=0; i < solarPanels.Count; i++){ 
        solars += solarPanels[i].MaxOutput;
        if ( maxPowerSolar < solarPanels[i].MaxOutput ) {
            maxPowerSolar = solarPanels[i].MaxOutput;
        }
    } 

    float batteri = 0;
    for (int i=0; i < batteries.Count; i++){ 
        batteri += batteries[i].CurrentStoredPower;
    } 
    
    // Write Text Battery
    textB += "\nNumber: " + batteries.Count; 
    textB +=  "\nTotal kW: " + batteri * 1000; 
    // Write Text SolarPanel
    textS += "\nNumber: " + solarPanels.Count; 
    textS += "\nTotal kW: " + solars * 1000;
    textS += "\nPanel Average kW: " + ( solars * 1000 / solarPanels.Count );
    textS += "\nPanels Efficacity %: " + ( solars * 1000 / solarPanels.Count ) * 100 / maxCollectedSolarPanel;
    textS += "\nBest Panel kW: " + maxPowerSolar * 1000;

    foreach ( IMyTextPanel lcd in lcds ) {
        if ( lcd.CustomName.Equals("[SE LCD SOLAR]") ) {
            // Write text in displays 
            lcd.WritePublicText(textS, false);
            // Show public text in displays 
            lcd.ShowPublicTextOnScreen(); 
        } else if ( lcd.CustomName.Equals("[SE LCD BATTERY]") ) {
            lcd.WritePublicText(textB, false); 
            lcd.ShowPublicTextOnScreen(); 
        }
    }
} 

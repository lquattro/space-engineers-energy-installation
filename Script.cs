static HashSet<IMyInventory> invenLocked = new HashSet<IMyInventory>();
// static List<IMyInventoryItem> stacks;

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
    
    float solars = 0;
    float batteri = 0;
    for (int i=0; i < solarPanels.Count; i++){ 
         solars += solarPanels[i].MaxOutput;
    } 

    for (int i=0; i < batteries.Count; i++){ 
        batteri += batteries[i].CurrentStoredPower;
    } 
    
    textB += "\n number " + batteries.Count; 
    textB +=  "\n " + batteri * 1000 + " kW"; 
    textS += "\n number " + solarPanels.Count; 
    textS += "\n " + solars * 1000 + " kW";

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

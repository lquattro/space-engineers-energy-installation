# Space Engineers : Source Energy

## Description :
This lightweight script allows you to display on lcd screens information on the power consumption and energy saving of the blocks of a ship / station.

This script is at the beginning and for now it has only this elements:
* Time clock
* Display of energy solar panels
* Display of energy stored in the batteries
* Display the different energy consommation for the batteries
* Display reactors power and Quantity Uranium

This script is set to display the desired values on any lcd screen which contains this specials tag names in name LCD:
* __CALL SCRIPT in LCD [obligatory] : __
```[SE]```
* Time clock: 
```CLOCK```
* Solar Panel: 
```SOLAR```
* Battery: 
```BATTERY```
* Consommation: 
```CONSO```
* Reactor: 
```REACTOR```

### Example:
In all LCDs where do you need to see the script data, you need to change Name LCD by :
* example 1:
```[SE] LCD ;CLOCK;SOLAR;REACTOR;```
* example 2:
```[SE] All you want to write ;solar;conso;CLOCK;```

* __It's important to separate the different module by a semicolon__ ```;```
* __In name lcd, you can call module (clock, solar...) in lower case or upper case (it's your choice)__

## Next Feactures :
- Conso Thrusters
- Jump Drive Power and Loading

## LICENSE

[APACHE LICENSE VERSION 2.0](LICENSE)
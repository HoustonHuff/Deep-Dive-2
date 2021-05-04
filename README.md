##**Data Pipeline**  
&nbsp;&nbsp;One of the main requirements of this project was to display sensor data such as depth and heading, from the accompanying sensors, onto a set of AR glasses.  
![alt text](https://github.com/HoustonHuff/Deep-Dive-2/blob/main/Resources/Data_Pipeline_Chart.png)

*Two issues currently exist in the pipeline  
&nbsp;&nbsp;1) Data extraction from the I2C bus was not able to be fully tested due to sensor malfunctions.  
&nbsp;&nbsp;&nbsp;&nbsp;-Only a successful connection with the sensors has been established which we confirmed with the following I2C commands[Link]( https://www.abelectronics.co.uk/kb/article/1092/i2c-part-3---i-c-tools-in-linux).  
&nbsp;&nbsp;&nbsp;&nbsp;But Data can not be extracted as of yet due to the rpi crashing sporadically when the sensors are plugged in.  
&nbsp;&nbsp;2) Passing data from the Rpi's application to the ThirdEye AR Glasses is not yet functional.  
&nbsp;&nbsp;&nbsp;&nbsp;-Currently one method of passing data via bluetooth from the Rpi to the glasses android app seems promising.  

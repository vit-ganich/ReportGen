## ReportGenerator
Console application, scanning a folder, parsing filenames and returns a csv-file with files list.</br>

### Example:</br>
We have the folder C:\Test Results\CI, which contains subfolders like </br>
10_22_2018</br>
10_23_2018 </br>
10_24_2018, etc. </br>
</br>
Each of them contains some subfolders like </br>
CLIENT433_TEST 1 </br>
CLIENT434_TEST 2</br>
CLIENT434_TEST 3, etc.</br>

In it's turn, each of the subfolders contains diffrent trx-files like this:</br>

10.01-Test sample_B1.1.100.1_87_0_0.trx</br>
10.02-Test sample_B1.1.100.1_52_5_6.trx</br>
20.01-Test sample_B1.1.100.1_33_0_0.trx</br>
20.02-Test sample_B1.1.100.1_31_0_0.trx</br>
99.09-Test sample_B1.1.100.1_63_35_0.trx</br>

The program parses the full path to each file and writes each file in summary.</br>
Full path example - C:\Test Results\CI\10_22_2018\CLIENT435_TEST 3\99.09-Test sample_B1.1.100.1_63_35_0.trx </br>
In addition, if the trx-file contains error records, the first five of them are being recorded in summary report.</br>

#### Example of final csv-report (opens in Excel):</br>

![Screenshot](sample.jpg)

I doubt that this program can be useful for anybody else, but it served me quite well.</br>
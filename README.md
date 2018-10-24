## ReportGenerator
Console application, scanning a folder, parsing filenames and returns a csv-file with files list

### Example:
We have the folder \10_22_2018\, which contains some subfolders (\QWE CM_1_COMPUTERNAME43\, \Test TT1_COMPUTERNAME43\, etc.) with files like this:</br>

10_22_2018\QWE CM_1_COMPUTERNAME43\11.18-Testtesttest20_01_Hamster_Chrome_B0.0.08295.9-141-87-0-0.trx</br>
10_22_2018\Test TT1_COMPUTERNAME43\11.22-MPCM Hamsters_0111_Oracle_Chrome_B0.0.08295.9-45-52-5-4.trx</br>
10_22_2018\Test TT1_COMPUTERNAME43\11.22-MPCM Hamsters_0112_Oracle_Chrome_B0.0.08295.9-43-55-6-4.trx</br>

On the output the application generates a csv-file (opens in Excel) like this:</br>

QA_Client		CI Group	Test name								Build Version	Time	Passed	Failed	Skipped	Result</br>
COMPUTERNAME43	QWE CM_1	11.18-Testtesttest20_01_Hamster_Chrome	B0.0.08295.9	141		87		0		0		PASSED</br>
COMPUTERNAME43	Test TT1	11.22-MPCM Hamsters_0111_Oracle_Chrome	B0.0.08295.9	45		52		5		6		FAILED</br>
COMPUTERNAME43	Test TT1	11.22-MPCM Hamsters_0112_Oracle_Chrome	B0.0.08295.9	43		55		6		4		FAILED</br>

I doubt that this program can be useful for anybody else, but it served me quite well.
call config.bat
start %browser% %link8%
:loop
tasklist /fi "windowtitle eq Cisco Webex Meetings" > tmp.txt
findstr /m "atmgr.exe" tmp.txt >Nul
if %errorlevel%==1 (
	timeout /t 2
	goto loop
)
taskkill /IM %browser%
del tmp.txt
taskkill /IM cmd.exe
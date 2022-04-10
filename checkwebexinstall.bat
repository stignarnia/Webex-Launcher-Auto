wmic product get name > webexisinstalled.txt
find "Webex" webexisinstalled.txt
if %errorlevel%==1 (
	start https://www.webex.com/downloads.html
	start errormessage.vbs
	del webexisinstalled.txt
 	taskkill /IM cmd.exe
)
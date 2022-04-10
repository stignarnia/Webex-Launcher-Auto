@echo off

if not exist webexisinstalled.txt (
	echo Controllo se Webex e' installato
	call checkwebexinstall.bat
)

call config.bat

@Mode 48,15
@Title %~n0
Batbox /h 0

call Button.bat  4 3 %pulsante1% 30 3 %pulsante2% 17 9 %pulsante3% # Press
call InputTranslator.bat

:: Check for the pressed button 
if %bottone%==1 ("Webex Launcher 1.bat")
if %bottone%==2 ("Webex Launcher 2.bat")
if %bottone%==3 ("Webex Launcher 3.bat")

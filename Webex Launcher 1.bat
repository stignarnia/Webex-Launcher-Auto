@Echo off
call config.bat

@Mode 48,15
@Title %~n0
Batbox /h 0

call Button.bat  4 3 %pulsante4% 30 3 %pulsante5% 17 9 %pulsante0% # Press
call InputTranslator.bat

:: Check for the pressed button 
if %bottone%==1 (start prof4)
if %bottone%==2 (start prof5)
if %bottone%==3 (exit)

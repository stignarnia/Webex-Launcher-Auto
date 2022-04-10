@echo off
setlocal EnableDelayedExpansion

:retry
GetInput
set /A "input=-%errorlevel%"
if %input% gtr 0 (
	set /A "row=input >> 16, col=input & 0xFFFF"
	if not !col! lss 32768 (
		goto retry
	)
) else (
	goto retry
)

if %row% gtr 9 (
	endlocal & set "bottone=3"
) else if %col% gtr 18 (
	endlocal & set "bottone=2"
) else (
	endlocal & set "bottone=1"
)
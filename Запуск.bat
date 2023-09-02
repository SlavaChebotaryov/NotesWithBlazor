@echo off

set NotesApiPath=%cd%\NotesApi
set NotesWithBlazorPath=%cd%\NotesWithBlazor

cd %NotesApiPath%
start dotnet run

cd %NotesWithBlazorPath%
start dotnet watch run

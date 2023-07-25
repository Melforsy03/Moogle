
#!/bin/bash

if [$1 == "run"] ; then
cd ..
dotnet watch run --project MoogleServer
fi
if [$1 == "report"] ; then
cd Informe

latexmk -pdf Informe.tex

rm Informe.aux Informe.fdb_latexmk Informe.fls Informe.log

xdg-open Informe.pdf
fi
if [$1 == "show_report"] ; then 

cd Presentacion

latexmk -pdf Presentacion.tex

rm Presentacion.aux Presentacion.fdb_latexmk Presentacion.fls Presentacion.log

xdg-open Presentacion.pdf
fi
if [$1 == "clean"] ; then 
#compilar el programa de c#

mcs programa.cs

#Ejecutar el progrma 

mono programa.exe

#limpiar archivos generados 

rm programa.exe programa.mdb

fi






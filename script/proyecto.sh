
#!/bin/bash

if [ $1=="run" ] ; then

cd ..
dotnet watch run --project MoogleServer

elif [ $1=="report" ] ; then

cd Informe

latexmk -pdf Informe.tex

elif [ $1=="slides" ] ; then 

cd Presentacion

latexmk -pdf Presentacion.tex


elif [ $1=="clean" ] ; then 

rm Informe.aux Informe.fdb_latexmk Informe.fls Informe.log Informe.pdf

rm Presentacion.aux Presentacion.fdb_latexmk Presentacion.fls Informe.log Presentacion.pdf

rm -r obj 

rm -r bin


elif [ $1=="show_slides" ] ; then 

cd Presentacion

#si pasa un parametro del visualizador 
if [ -n "$2" ] ; then 

 "$2" Presentacion.pdf

else 
#visualizador por defecto

xdg-open Presentacion.pdf

fi
 #show report

 else 

if [ $1=="show_report" ] ; then 

cd Informe

if [ -n "$2" ] ; then 

"$2" Informe.pdf

else 

xdg-open Informe.pdf

fi

fi

fi


#!/bin/bash

read A

cd ..

if [ "$A" == "run" ] ;
 then
   dotnet watch run --project MoogleServer
fi

if [ "$A" == "report" ] ;
then
 cd Informe
 latexmk -pdf Informe.tex
  cd ..
fi

if [ "$A" == "slides" ] ; 
then 
 cd Presentacion
 latexmk -pdf Presentacion.tex
  cd ..
fi

if [ "$A" == "clean" ] ; 
then 
cd Informe 
 rm Informe.aux Informe.fdb_latexmk Informe.fls Informe.log Informe.pdf
cd ..
cd Presentacion

 rm Presentacion.aux Presentacion.fdb_latexmk Presentacion.fls Informe.log Presentacion.pdf Presentacion.txt

cd ..
 rm -r obj 

 rm -r bin
fi

if [ "$A" == "show slides" ] ;
then 
 cd Presentacion
 read B
 #si pasa un parametro del visualizador 
 if [ -n "$B" ] ;
 then 
 "$B" Presentacion.pdf

 else 
 #visualizador por defecto

 xdg-open Presentacion.pdf

 fi
 #show report
 cd ..
fi
 
if [ "$A" == "show report" ] ;
 then 
 read B
 cd Informe
 if [ -n "$B" ] ; 
 then 
 "$B" Informe.pdf

 else 
 xdg-open Informe.pdf
 fi
 cd ..
fi



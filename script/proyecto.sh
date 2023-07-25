
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

 rm Presentacion.aux Presentacion.fdb_latexmk Presentacion.fls Presentacion.log Presentacion.pdf 

cd ..
 rm -r obj 

 rm -r bin

fi

if [ "$A" == "show slides" ] ;

then 

 cd Presentacion

 read B
 #si pasa un parametro del visualizador 
 if [  -n "$B" ] ;

 then 

 $B Presentacion.pdf

else

 start Presentacion.pdf

 
 #show report
 

fi
cd ..
fi

if [ "$A" == "show report" ] ;

 then 

 cd Informe

 read B
 if [ -n "$B" ] ; 

 then 

 $B Informe.pdf

else 

 start Informe.pdf

fi
 cd ..
fi



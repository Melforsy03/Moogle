using System.Xml.Linq;
using System.Security.Cryptography.X509Certificates;
using System.ComponentModel;
using System.Net.Http;
using System.Net.WebSockets;
using System.Data;
using System.ComponentModel.DataAnnotations;
using System.Collections;
using System.Collections.Generic;
using System;
using TAMAL;
using MoogleEngine;
using Name;
using operadores;

namespace TACOS
{
    class consulta
{
  
   //obtengo los valores del query y los meto en un array
public static  double  [] asignacion(Dictionary <string, double > query , Dictionary <string, double > valores , string queryto)
{
        List < string > quer = new List<string>(){queryto};

        quer = queryto.Split(' ').ToList();

         double  [] valor = new  double  [valores.Count];
        int k = 0;
     foreach (var kvp in valores)
     {
            if(quer.Contains(kvp.Key))
             valor[k] = query[kvp.Key];
            else{
            valor[k] = 0;
            }
            k++;
     }

        return valor;

}    
  //le destino los escore a los documentos
static public Dictionary<string ,  double > escoreando (Dictionary<string ,Dictionary< string,  double >> query ,Dictionary<string ,Dictionary< string,  double >> valores, string [] name ,string queryto,string [] docs,string virgen,Dictionary<int ,string > voc)
{
  string a = "~";
  string b = "!";
  string c = "^";
  string d = "*";
      Dictionary<string , double > clave = new Dictionary <string,  double >();
      List <Dictionary<string , double  >> reina = new List<Dictionary<string, double >>();
    
       for (int k = 0; k < name.Length; k++)
       {
        
          //valores del query en un docuemnto
          double  [] tif  = asignacion(query[name[k]],valores[name[k]],queryto);
        //valores de un documento
         double  [] cer = ceri(valores[name[k]]);
      //calculo el escore de cada documento 
         double cos = Escore.CalSimilitudCos(cer,tif);
      
       // miro si hay algun operador en el query
        if( virgen.Contains(a) ||virgen.Contains(b)||virgen.Contains(c)||virgen.Contains(d))
        cos =  operando.modificar(virgen,docs[k],cos,voc);
       
        clave.Add(name[k],cos);
       
        
       }
       
     
     return clave ;
}
//introduzco los valores de un documento en un array
public static  double  []  ceri (Dictionary<string , double > doc)
{
   double  [] mer = new  double [doc.Count];
  int k = 0;
  
 foreach ( var kvp in doc)
 {
  mer[k] = doc[kvp.Key];
   k++;
 }
 return mer ;
}
//escojo la palabra de mayor relevancia en el texto 
 public static string best(Dictionary <string, double > doc , string queryto )
 {
   List<string> query = new List<string>{queryto};
   query = queryto.Split(' ').ToList();
   List<string> palabras = new List<string>();
    double  valor = 0;
    double valor1 = 0;
   string word =" ";
    for (int i = 0; i < query.Count; i++)
    {
      if(doc.ContainsKey(query[i]))
      palabras.Add(query[i]);
    }
  for (int i = 0; i < palabras.Count; i++)
  {
    valor = doc[palabras[i]];
    if(valor1 < valor)
    {
      word = palabras[i];

      valor1 = valor;
    }
  
  }

  string pala = word;
  return pala;
 }
  //obtengo el snipet del la palabra mas relevante del documento
 public static string snipet(string word,string doc)
 {
  List <string> docu = new List<string>(){doc};
  string snipet = "";
  List<int> enteros = new List<int>();
    docu = doc.Split(' ').ToList();
  int index = 0;

  for (int j = 0; j < docu.Count; j++)
  {
      if(word == docu[j])
      {
        
          enteros.Add(j);
          index = enteros[0];
      }
  }
  if (enteros.Count == 0)
  {
    index = 0;
  }


 for (int i = index ; i < docu.Count; i++)
 {
  if(i != index + 20)
  {
   snipet += " " + docu[i];

  }
 }
  return snipet ;

 }
// retorno el snipet
public static string  bestSnipet(Dictionary <string ,Dictionary<string, double >> valores , string query , string  name , string contenido )
{
  string  pedazo = " ";
  string palabra = " ";
  int k = 0;
  for (int i = 0; i < valores.Count; i++)
  {
    palabra = best(valores[name],query);
    pedazo = snipet(palabra,contenido);
    k++;
  }
  return pedazo;
}
  //metodo auxiliar donde asocio los documentos con sus nombres
 public static Dictionary <string ,string > melani (string [] docs , string [] name )
    {
      Dictionary< string ,string > lugares = new Dictionary<string,string>();
      for (int i = 0; i < name.Length; i++)
      {
        lugares.Add(name[i],docs[i]);
      }
      return lugares;

    }
   
}
}

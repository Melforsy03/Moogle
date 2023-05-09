

using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Net.Http;
using TAMAL;
using MoogleEngine;
using Name;
using TACOS;



namespace MoogleEngine
{
 public static class Moogle
 {

   //public static string [] docs = moggle.quitar();

    public static SearchResult Query(string texto) 
  {
    List <SearchItem> items =new List<SearchItem>();
    
    string query = moggle.quitado(texto);
    //string [] docs = moggle.quitar();
    string [] name = moggle.nombres();

    Dictionary<int,string> voc = Escore.vocabulary(moggle.textos);
    
    string cerca = Escore.cercano(query,voc);
    
    Dictionary<string ,Dictionary<string , double >> TFIDF = Escore.TFIDF(voc ,moggle.textos ,name);
    
    Dictionary<string ,Dictionary< string,  double >> queryto = Escore.vectors(cerca,moggle.textos,name);
    
    Dictionary <string , double > escor = consulta.escoreando(queryto,TFIDF,name,cerca,moggle.textos,texto,voc );
    
    double [] arr = new  double [name.Length];
    
    Dictionary <string, double > ordenado = new Dictionary<string, double>();
    
    Dictionary <string, string > lugares = consulta.melani(moggle.textos, name);
    
    double [] array = Escore.obtener(escor , arr);
    
    Array.Sort(array);
   
    Array.Reverse(array);
  
   List <string> verificacion = new List<string >();


   for (int i = 0; i < array.Length; i++)
   {
    string key = "";
  
    foreach (var kvp in escor)
    {
      if(kvp.Value == array[i] && !verificacion.Contains(kvp.Key))
      {
          key = kvp.Key;
   
         verificacion.Add(kvp.Key);
      }
     
    }
      
   }
   for (int i = 0; i < array.Length; i++)
   {
    
     string snippet = consulta.bestSnipet(TFIDF,query,verificacion[i],lugares[verificacion[i]]);
   
     SearchItem random = new SearchItem( verificacion[i], snippet, array[i]);
     if(array[i] != 0)
     items.Add(random);
     }

        SearchItem [] ar = items.ToArray();
        return new SearchResult(ar,cerca);
  }
  }
   
 }

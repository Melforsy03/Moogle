
using System.Diagnostics.Contracts;
using System.Reflection.Metadata;
using System.Threading.Tasks.Sources;
using System.Net.Security;
using System.Configuration.Assemblies;
using System.Net.WebSockets;
using System;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Tracing;
using System.Text;
using System.Collections.Generic;
using Name;
using TACOS;
using operadores;
namespace TAMAL
{
   
   class Escore
 {
  
      
    
    //obtiene el vocabulario general del corpus
 public static Dictionary<int,string> vocabulary(string [] docs)
{ 
   int l = 0;
   Dictionary <int,string> vocab = new Dictionary<int,string>();

   for (int i = 0; i < docs.Length; i++)
   {
       List <string> removidas = new List<string>();

       removidas = docs[i].Split(' ').ToList();

    for (int j = 0; j < removidas.Count; j++)
    {
        if(!vocab.ContainsValue(removidas[j]))
        {
        vocab.Add(l,removidas[j]);
        l++;
        }
      }
      
   }
 
    return vocab;
}

//obtiene el tfidf del vector del query
 public static  Dictionary <string,Dictionary<string , double >> vectors(string query ,string [] docs, string [] name )
 {
    List <string> cadena = new List<string>(){query};
    cadena = query.Split(' ').ToList();
    Dictionary <string , double > TFIDF= new Dictionary<string , double >();
    Dictionary <string ,Dictionary<string, double >> vector = new Dictionary<string ,Dictionary<string , double >>();

      for (int k = 0; k < docs.Length; k++)
      {
         TFIDF = dicc(query,docs[k],docs);

         vector.Add(name [k],TFIDF);
        
      }
       return vector;
 }
 //introduce los tfidf del query por documentos
 static Dictionary<string, double > dicc (string query, string docs ,string [] doc)
 {
  
    Dictionary <string , double > TFIDF= new Dictionary<string , double >();
    List<string> palabra = new List<string>{query};
    palabra = query.Split(' ').ToList();
  
    for (int i = 0; i < palabra.Count; i++)
    {
    
      double tf = TF(palabra[i],docs,doc);
      if(palabra[i] == String.Empty) tf = 0;
     
        double idf = IDF(palabra[i] , doc);
     
      double tfidf = tf*idf;
      if(tf == 0 && idf == 0) tfidf = 0;
     if(!TFIDF.ContainsKey(palabra[i]))
      TFIDF.Add(palabra[i],tfidf);
    }
 
    return TFIDF;

 }
 //obtener tf
 public static  double  TF(string palabra ,string documento ,string [] docs)
 {
   float con = 0;

   List<string> lista = new List<String>{documento};

   lista = documento.Split(' ').ToList();

   for (int i = 0; i < lista.Count; i++)
   {
    if(lista[i] == palabra)
    con++;
   }
   
    double  TF = con/docs.Length ;
     
  return TF;

 }
 //obtener IDF
 public static double IDF(string palabra , string [] documento)
 {
   
     double contar = 0;
    for (int i = 0; i < documento.Length; i++)
    {
        if(documento[i].Contains(palabra))

        contar++;
    }
    double CantDocs = documento.Length;

    double IDF = Math.Log(CantDocs /contar + 1);

    if(palabra == String.Empty)IDF = 0;
    return IDF;
 }
 //valores de las palabras en los docuemntos
 public static Dictionary<string ,Dictionary<string , double >> TFIDF (Dictionary <int,string> vocab , string [] docs , string [] name)
 {
   Dictionary<string ,Dictionary<string, double >> valores = new Dictionary<string ,Dictionary<string, double >>();
   Dictionary<string , double > valor = new Dictionary<string, double >();
   int k = 0;
   
   for (int i = 0; i < docs.Length; i++)
   {
         
      for (int j = 0; j < vocab.Count; j++)
      {
         if (docs[i].Contains(vocab[j]))
         {
            try
         {
            double tf = TF(vocab[j] , docs [i] ,docs);

             double idf = IDF(vocab[j],docs);
         
           double  tfidf = tf*idf;
            if(vocab[j] != " " )
            valor.Add(vocab[j],tfidf);
         }
             catch (System.Exception)
         {
         }
         }
         }
      
         valores.Add(name[k],valor);
      k++;
    }
     return valores;
 }
    //calculo de similitud entre dos vectores
   public  static  double  CalSimilitudCos( double  [] query ,  double  [] documento)
  {
         double  producto = PRODUCTO(query,documento);
        
         double  magnitudA = MAGNITUD(documento);
        
         double  magnitudB = MAGNITUD(query);
      
         double  prod =  producto /magnitudA*magnitudB  ;
        
        return prod;

  }
    private static  double  PRODUCTO( double  [] query, double  [] documento )
    {
         double  producto = 0;
        for (int i = 0; i < query.Length; i++)
        {
          if(query[i] == 0 && documento[i] == 0)documento[i] = 1;
           producto += query[i]*documento[i];
          
        }
        
        return producto;
    }
    private static double MAGNITUD( double  [] vector)
    {
        return Math.Sqrt(PRODUCTO(vector,vector));
        
    }
      //distancia entre dos palabras//tiene poda 
     public static int levenshteinDistance(string salida , string llegada)
    {
      int distancia = int.MaxValue;
  LD(salida , llegada ,salida.Length-1,llegada.Length - 1,ref distancia ,0);
  return distancia;

  void LD(string s , string l ,int i ,int j , ref int mejor_distancia , int distancia_acum)
  {
    if(distancia_acum >= mejor_distancia || distancia_acum >= s.Length/2)return;
    if (i==-1)
    {
      mejor_distancia = Math.Min(distancia_acum + j + 1,mejor_distancia);return ;};
      if (j== -1)
      {
        mejor_distancia = Math.Min(distancia + i + 1,mejor_distancia); return ;};
      
      if (s[i] ==l[j])
      {
        LD(s, l ,i-1,j -1,ref mejor_distancia,distancia_acum);return ;}
        else
        {
          distancia_acum+=1;
          LD(s,l,i-1,j-1,ref mejor_distancia,distancia_acum);
          LD(s,l,i,j-1,ref mejor_distancia,distancia_acum);
          LD(s,l,i-1,j,ref mejor_distancia,distancia_acum);
       }
    }
    }
    // obtiene el string mas cercano a la palabra
     public static string cercano(string query ,Dictionary<int,string> voc)
    {
     string palabra ="";
     string texto = "";
     string variable = "";
     
      List<string> consulta = new List<string>();

         consulta = query.Split(' ').ToList();
        Console.WriteLine(consulta.Count);
        for (int i = 0; i < consulta.Count; i++)
        {
          if (consulta[i] == String.Empty)
          {
            consulta.Remove(consulta[i]);
          }
        }
      for (int i = 0;  i< consulta.Count  ; i++)
      {
        for (int j = 0; j < voc.Count; j++)
        {
  
          int dist = Escore.levenshteinDistance(consulta[i],voc[j]);
          int min = consulta[i].Length;
          int max = 3;
          if(dist < max && voc[j].Length == min)
          {
          max = dist;
          variable = voc[j];
          palabra = variable;
          }
          }
        
        texto += " " + palabra;
      
        if(texto == " ") texto = "NOT FOUND";
      }
      return texto;
    }
    
    // metod auxiliar para almacenar los escores en un array
    public static double [] obtener(Dictionary<string,double> escor, double [] array)
    {
      int i = 0;
     foreach (var kvp in escor)
     {
      array[i] = kvp.Value;
      i++;
     }
   
      return array;
    
    }
}
}





using System.Data.SqlTypes;

using System.Threading;
using System.ComponentModel;
using System.Net.Http;
using System.Xml;
using Microsoft.VisualBasic.CompilerServices;
using System.Runtime.Intrinsics.X86;
using System.Collections.Specialized;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography;
using TAMAL;
namespace operadores
{
   public class operando
    {
        
        //encuentra los operadores y los desaparece 
      public static string desaparecer(string query)
    {
           string [] quitar = new string[4]{"!","^","*","~"};
             List<string> lista = new List <string>(){query};
            lista = query.Split(' ').ToList();

            for (int i = 0; i < quitar.Length ; i++)
             {
                for (int j = 0; j < query.Length; j++)
                {
                  query = query.Replace(quitar[i] ,String.Empty);
                
                }
             }
          return query;
    }

        //calcula cuantos caracteres hay entre dos palabras
    public static int distancia (List <string> frase , string documento)
    {
        
            int distance = subStrin( frase[0] , frase[1], documento);
            return distance;
    }

        //cuenta la cantidad de caracteres entre dos palabras 
    
    public static int subStrin (string palabra1 ,string palabra2 ,string doc)
    {
          int count = doc.Length;
          int menor = 0;
     List<int> a = new List<int>();
     List<int> b = new List<int>();
     List < string > separados = new List<string>(){doc};

     separados = doc.Split(' ').ToList();

    for (int i = 0; i < separados.Count; i++)
    {
       
    if (!doc.Contains(palabra1) || !doc.Contains(palabra2))
    {
        count = doc.Length;
    }
    else
    {
      if(separados[i] == palabra1)
      {
        a.Add(i);
      }  
      if(separados[i] == palabra2)
      {
        b.Add(i);
      }
      for (int k = 0; k < a.Count ; k++)
      {
        for (int j = 0; j < b.Count; j++)
        {
            menor = a[k] - b[j];
            if (menor < 0) menor = menor*(-1);
            if(menor < count) count = menor;
        }
      }
    
    }
}
return count;
    
    }
        //me saca la palabra que debe incluir y quita el operador
    public static List <string> inclusion(string query)
    {
            char a = '^';
             List <string> cosas = new List<string>();
            cosas = query.Split(' ').ToList();
             
            for (int i = 0; i < cosas.Count; i++)
            {
                 if(!cosas[i].Contains(a))
                  cosas.Remove(cosas[i]);
            }
            //nota para mi : habia veces en donde no removia la palabra asi q volvi a reviasar//mejorar 
            for (int i = 0; i < cosas.Count; i++)
            {
                 if(!cosas[i].Contains(a))
                cosas.Remove(cosas[i]);
            }
          
             return cosas;
      }
    
    
    //quita el operador ! y te saca las palabras q lo contienen
    public static  List<string> quitar(string query)
    {
        char a = '!';
       

             List <string> cosas = new List<string>();
            cosas = query.Split(' ').ToList();

            for (int i = 0; i < cosas.Count; i++)
            {
                
                if(!cosas[i].Contains(a))
                cosas.Remove(cosas[i]);
            }
           //nota para mi : habia veces en donde no removia la palabra asi q volvi a reviasar//mejorar
            for (int i = 0; i < cosas.Count; i++)
            {
             if(!cosas[i].Contains(a))
                cosas.Remove(cosas[i]);
            }
            for (int i = 0; i < cosas.Count; i++)
            {
                cosas[i] = desaparecer(cosas[i]);
            
            }
             return cosas;
    }
    //me saca la palabra que tiene el operador *
    public static List <string> asterisco(string query)
    {
            string a = "*";

             List <string> cosas = new List<string>();
            cosas = query.Split(' ').ToList();

            for (int i = 0; i < cosas.Count; i++)
            {
              
                if(!cosas[i].Contains(a))
                cosas.Remove(cosas[i]);
            }
           //nota para mi : habia veces en donde no removia la palabra asi q volvi a reviasar //mejorar
            for (int i = 0; i < cosas.Count; i++)
            {
                 if(!cosas[i].Contains(a))
                cosas.Remove(cosas[i]);
            }
             return cosas;
    }
        
        //encuentra las dos palabras en el que esta el operador  
    public static List <string> OPdistance(string query , Dictionary <int , string > voc)
    {
            string a = "~";
            List<string> melani = new List<string>();
             string pal1 = "";
             string pal2 = "";
            melani = query.Split(' ').ToList();
            int index = 0;

            for (int i = 0; i < melani.Count; i++)
            {
                if(melani[i] == a )
                {
                 index = i;
                }
              
            } 
            List <string> palabras = new List<string>();
            if(index != 0)
            {
            pal1 = melani[index - 1];
            pal1 = Escore.cercano(pal1,voc);
            palabras.Add(pal1);
            }
            pal2 = melani[index + 1];
            pal2 = Escore.cercano(pal2,voc);
            palabras.Add(pal2);

        

          return palabras;
    }
       
       //almacena en un array la cantidad de * por palabras
    public static int [] prioridad(List <string> frase)
    {
             int [] contador  = new int[frase.Count] ;
             int cuenta = 0;
            for (int i = 0; i < frase.Count; i++)
            {
                for (int j = 0; j < frase[i].Length; j++)
                {
                    if(frase[i][j] == '*')
                    cuenta++;
                    contador[i] = cuenta;
                }
            }
            return contador;
    }
    //modifica el escore segun los operadores
    public static double modificar(string query,string documento ,double escore , Dictionary <int,string> voc)  
    {
       bool desaparece ;
       bool aparecer ;
   
       double escore2 = escore;
            
            if(query.Contains("!"))
             {
                List <string> listado = new List<string>();
                listado = quitar(query);
        
               for (int i = 0; i < listado.Count; i++)
                {
                    listado[i] = Escore.cercano(listado[i],voc);
                 desaparece = saber(documento,listado[i]);
                
                if(desaparece == true) escore2 = 0;
                }
                
             }
              if(query.Contains("^") && escore2 != 0)
             {
             List <string> lista = new List<string>();
            
             lista = inclusion(query);
            

              for (int i = 0; i < lista.Count; i++)
            {
                lista[i] = desaparecer(lista[i]);
                lista[i] = Escore.cercano(lista[i],voc);
                aparecer = saber(documento,lista[i]);

                if(aparecer == false) escore2 = 0;
            }

            }
                if(escore2 != 0)
                {
                     escore2 = valor(query,documento,escore,voc);
                }

            
        return escore2;
    }
        //evaluo los operadores de cercania y de prioridad
    public static double valor(string query ,string documento , double escore,Dictionary<int ,string> voc)
    {
            double escore1 = escore; 
            double escore2 = escore;
        //proceso el escore segun el  operador *
        if(query.Contains("*"))
        {
        List <string> frase = asterisco(query);

        int [] contadores = prioridad(frase);
    
        for (int i = 0; i < frase.Count; i++)
        {       
            frase[i] = desaparecer(frase[i]);
            frase[i] = Escore.cercano(frase[i],voc);
            if(frase[i] != String.Empty)
            {
            if(documento.Contains(frase[i]))
            {
             escore1 += escore*contadores[i]*Math.Pow(contadores[i],2);
           
            }
            }
        }
        }
        double escore3 = 0;
        if(query.Contains("~"))
        {
        escore3 = escore;
        //proceso el escore segun el operador de distancia 
        List <string> palabras = OPdistance(query,voc);

        int dista = distancia(palabras,documento);
            Console.WriteLine(dista);
        escore3 = escore/(dista + 1);
        }
        if(escore1 != 0 && escore2 != 0)
        escore = escore1 + escore3;
         Console.WriteLine(escore);
         return escore;
        
    }
    //metodo q me dice si el operadores esta
      static bool saber(string docuemnto ,string palabra)
    {
        if(docuemnto.Contains(palabra))
        return true;
        else 
        {
        return false;
        }
    } 
  }

}

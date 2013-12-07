/*Funciones para convertir a mayusculas y minusculas */
function M(txt){
    txt.value=txt.value.toUpperCase();
}
function min(txt){
    txt.value=txt.value.toLowerCase();
}
/*---------------------------------------------------*/

function seleccionarCombo(combo, find) {
   var cantidad = combo.length;
   for (i = 0; i < cantidad; i++) {
      if (combo[i].value == find) {
         combo[i].selected = true;
      }   
   }
}

function ContadorTexto(txt, maxlength){
    if(txt.value.length > maxlength){
        txt.value = txt.value.substring(0, maxlength);
    }
}

function SoloUsuario(){
   k = event.keyCode;
    if (k>=65 && k<=90){   } //A-Z
    else if(k>=97 && k<=122){   } //a-z
    else
    {
        event.keyCode=0;
    } 
}

function SoloLetras(Contador, obj, len){
   k = event.keyCode;
    if (k>=65 && k<=90){   } //A-Z
    else if(k>=97 && k<=122){   } //a-z
    else
    {
        event.keyCode=0;
    } 
}

function SoloNombres(Contador, obj, len){
   k = event.keyCode;
   
   switch(k){
    case 32:
    case 39:
    case 241: //ñ
    case 209: //Ñ
        break;
    default:
        if (k>=65 && k<=90){   }
        else if(k>=97 && k<=122){   } //a-z
        else if(k>=192 && k<=197){   } //A
        else if(k>=200 && k<=207){   } //E  I
        else if(k>=210 && k<=214){   } //O
        else if(k>=217 && k<=220){   } //U
        
        else if(k>=224 && k<=229){   } // a
        else if(k>=232 && k<=246){   } // e i ñ o
        else if(k>=250 && k<=252){   } // u
        else
        {
            event.keyCode=0;
            return false;
        } 
   }
   if(Contador==1){
    ContadorTexto(obj,len)
   }
   return true;
}

function SoloDireccion(Contador, obj, len){
   k = event.keyCode;
   
   switch(k){
    case 32:    
    case 45://  -
    case 35://  #    
    case 46://  .
    case 209: //Ñ
        break;
    default:
        if (k>=65 && k<=90){   } //A-Z
        else if(k>=48 && k<=57){   }  //0-9
        else if(k>=97 && k<=122){   }  //a-z
        else if(k>=192 && k<=197){   } //A
        else if(k>=200 && k<=207){   } //E  I
        else if(k>=210 && k<=214){   } //O
        else if(k>=217 && k<=220){   } //U
        
        else if(k>=224 && k<=229){   } // a
        else if(k>=232 && k<=246){   } // e i ñ o
        else if(k>=250 && k<=252){   } // u
        else
        {
            event.keyCode=0;
            return false;
        } 
   }
   if(Contador==1){
    ContadorTexto(obj,len)
   }
   return true;
}

//function SoloLetras(obj){
//    k = event.keyCode;
//    if (k>=48 && k<=57){
//       event.keyCode=0 
//    }
//}
    
function SoloEnteros(){
	k = event.keyCode;
	if(k>=48 && k<=57){}
	else{ event.keyCode=0; }
}

function SoloDecimales(obj){
	valor = obj.value;
	k = event.keyCode;
	if(k==46){
		if(valor.length==0){
			event.keyCode=0;
		}else{
			if(valor.indexOf(".")!=-1){
				event.keyCode = 0;
			}
		}
	}else{
		if(k>=48 && k<=57){ }
		else{ event.keyCode=0; }	
	}
}

function SoloTelefono(obj){
    //valor =obj.value;
	k = event.keyCode;
	if(k>=48 && k<=57){}
	else{ 
	    switch(k){
	        case 32://espacio
	        //case 40://  (
	        //case 41://  )
	        //case 45://  -
            //case 35://  #
            case 42://  *
	        return;
	    }
	    event.keyCode=0; 
	}
}
   
function ComparaFechas(dtf1, dtf2){
//la fecha 2 debe ser necesariamente mayor a fecha1
	fi = dtf1.split("/");
	ff = dtf2.split("/");
	_fechai = fi[0]*10000 + fi[1]*100 + fi[2];
	_fechaf = ff[0]*10000 + ff[1]*100 + ff[2];
	n = _fechaf - _fechai;
	if(n>0) return true;
	else return false;
}
function SoloFecha(obj){
   //valido para formato dd/mm/yyyy
   f = obj.value;
   switch(f.length){
    case 2: case 5:  
        f=f+"/"; 
        obj.value=f;
        break;
   }
}        
function SoloHora(obj){
	k = event.keyCode;
	if(k>=48 && k<=57){}
	else{ 
	    switch(k){
	        case 58:
	        return;
	    }
	    event.keyCode=0; 
	}  
}
function SoloURL(){
    k = event.keyCode;
	switch(k){
	    case 32: event.keyCode=0;
	}
}

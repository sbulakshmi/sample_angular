export interface Make {
    id: string;
    name: string;
    models:[{id:string,name:string}]
  }
  export interface Feature {
    id: string;
    name: string;
  }
  export interface KeyValuePair{
    id: number;
    name: string;
  }
  
  export interface Contact{
    email: string;
    name: string;
    phone: string;
  }
  
  export interface Vehicle{
    id: number,
    model:KeyValuePair;
    make:KeyValuePair;
    isRegistered:boolean;
    contact: Contact;
    lastUpdated: string;
    features:KeyValuePair[];
    }
  

  export interface QueryResult{
    totalItems:number;
    items: Vehicle[];
  }
  
    
  export interface SaveVehicle{
    id: number;
    modelId:number;
    makeId:number;
    isRegistered:boolean;
    contact: Contact;
    features:number[];
    }

    export interface Photo{
      id:number,
      fileName:string;
    }
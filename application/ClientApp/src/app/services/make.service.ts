import { KeyValuePair, QueryResult } from './../models/vehicle';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Make, Feature, Vehicle, SaveVehicle } from '../models/vehicle';

//import 'rxjs/add/operator/map';


@Injectable()
export class VehicleService {

  constructor(private http:HttpClient) { }
  getMakes(){
    return this.http.get<[Make]>('api/makes');
    //    .subscribe(result=>{
    //      return (result.);
    //    } );
  }
  getFeatures()
  {
    return this.http.get<[Feature]>('api/features');
  }

  getVehicle(id)
  {
    return this.http.get<Vehicle>('api/vehicles/GetVehicle/'+ id);
  }
  private getQueryString(obj)
  {
    var parts=[]
   // obj.forEach(element => {
     for(var prop in obj){
      var value= obj[prop];
      if(value!= undefined && value!=null)
      parts.push(encodeURIComponent(prop) + "=" + encodeURIComponent(value));
    }
    return parts.join("&");
  }
  getVehicles(filter)
  {
    return this.http.get<QueryResult>('api/vehicles/GetVehicles?'+ this.getQueryString(filter));
  }
  createVehicle(vehicle){
     return this.http.post("api/vehicles/CreateVehicle",vehicle,
     {
       
       observe: 'response'
     });
         //.subscribe(data=>{data},error=>{console.log(error); error})
  }

  updateVehicle(vehicle:SaveVehicle){
    return this.http.put("api/vehicles/UpdateVehicle/"+vehicle.id,vehicle,
     {
        observe: 'response'
     });
  }

  deleteVehicle(id)
  {
    return this.http.delete('api/vehicles/DeleteVehicle/'+ id);
  }
}

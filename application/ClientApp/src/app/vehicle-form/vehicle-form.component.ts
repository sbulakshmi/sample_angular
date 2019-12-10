import { Vehicle, SaveVehicle } from './../models/vehicle';
import { VehicleService } from './../services/make.service';
import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { ToastyService } from 'ng2-toasty';
//import 'rxjs/add/observable/forkJoin';
// // import { forkJoin } from 'rxjs';
import { Observable } from 'rxjs/Rx'

import * as _ from 'underscore';
//import 'rxjs/add/observable/forkJoin';
import { forkJoin } from 'rxjs/observable/forkJoin';

@Component({
  selector: 'app-vehicle-form',
  templateUrl: './vehicle-form.component.html',
  styleUrls: ['./vehicle-form.component.css']
})
export class VehicleFormComponent implements OnInit {
makes:any[];
vehicle :SaveVehicle = {id :0, modelId:0, makeId:0, isRegistered:false, features:[], contact:{name:'',phone:'', email:''}};
models:any[];
features:any[];


  constructor(private vehicleService:VehicleService
    , private route: ActivatedRoute
    , private router: Router
    , private toastyService: ToastyService) {
      route.params.subscribe(p=>{
        //debugger;
        this.vehicle.id= +p['id']||0;
      })
     }

  ngOnInit() {

    // var sources:any=[
    //   this.vehicleService.getMakes(),
    //   this.vehicleService.getFeatures()
    // ];
      
      // if(this.vehicle.id)
      // sources.push(this.vehicleService.getVehicle(this.vehicle.id));
      debugger;
    forkJoin(
      this.vehicleService.getMakes(),
      this.vehicleService.getFeatures(),
     
      (this.vehicle.id)?this.vehicleService.getVehicle(this.vehicle.id):Observable.of(null),
    ).subscribe(
      ([data0 , data1,data2])=>
      {
        
        this.makes=data0;
        this.features=data1;

         if(this.vehicle.id)
         {
          this.SetVehicle(data2);
          this.populateModel();
         }
        //this.vehicle = data[2];
      }
    );


    // this.vehicleService.getVehicle(this.vehicle.id).subscribe(v=>{
    //   this.vehicle=v;
    // })

    // this.vehicleService.getMakes().subscribe(result=>{
    //   this.makes = result;
    //  // console.log(this.makes);
    // });
    // this.vehicleService.getFeatures().subscribe(result=>{
    //   this.features = result;
    //  // console.log(this.features);
    // });
  }

  SetVehicle(v:Vehicle)
  {
    this.vehicle.id= v.id;
    this.vehicle.modelId=v.model.id;
    this.vehicle.makeId=v.make.id;
    this.vehicle.isRegistered = v.isRegistered;
    this.vehicle.contact= v.contact;
    this.vehicle.features = _.pluck(v.features,'id');
  }

  fillModel()
  {
    this.populateModel();
    console.log(this.vehicle);
    delete this.vehicle.modelId;

  }
  private populateModel()
  {
    var selectedMake = this.makes.find(m=>m.id==this.vehicle.makeId);
    this.models = selectedMake ? selectedMake.models:[];
  
  }

  onFeatureToggle(id, event)
  {
    if(event.target.checked)
      this.vehicle.features.push(id);
    else
    {
      var index=  this.vehicle.features.indexOf(id);
      debugger
      this.vehicle.features.splice(index,1);
    }
  }
  deleteVehicle(){
  if(confirm("Are you sure?")){
    this.vehicleService.deleteVehicle(this.vehicle.id).subscribe(x=> 
      {
        this.toastyService.success({
              title:"Success",
              msg:"Vehicle removed successfully",
              theme:"bootstrap",
              showClose:true,
              timeout:5000
            }
          )
        debugger;
        console.log(x);
        this.router.navigate(['/']);
      });
  }
}
  submit()
  {
    var result$ = (this.vehicle.id)? this.vehicleService.updateVehicle(this.vehicle): this.vehicleService.createVehicle(this.vehicle);
    result$.subscribe(x=> 
      {
        this.toastyService.success({
              title:"Success",
              msg:"Data was saved successfully",
              theme:"bootstrap",
              showClose:true,
              timeout:5000
            }
          )
       this.router.navigate(["/vehicles/"+ this.vehicle.id]);
      })
    
    // if(this.vehicle.id)
    // {
    //   this.vehicleService.updateVehicle(this.vehicle)
    //   .subscribe(x=> 
    //     {
    //       this.toastyService.success({
    //             title:"Success",
    //             msg:"Vehicle updated successfully",
    //             theme:"bootstrap",
    //             showClose:true,
    //             timeout:5000
    //           }
    //         )
    //       debugger;
    //       console.log(x);
    //     }
    //     //,
    //   // err=> {
    //   //   debugger;
    //   //   this.toastyService.error({
    //   //     title:"Error",
    //   //     msg:"An unexpected error occured",
    //   //     theme:"bootstrap",
    //   //     showClose:true,
    //   //     timeout:5000
    //   //   }
    //   //)
    //   //}
    //   ) ;
    // }
    // else
    // {
    //   this.vehicleService.createVehicle(this.vehicle)
    //   .subscribe(x=> 
    //     {
    //       debugger;
    //       console.log(x);
    //     }
    //     //,
    //   // err=> {
    //   //   debugger;
    //   //   this.toastyService.error({
    //   //     title:"Error",
    //   //     msg:"An unexpected error occured",
    //   //     theme:"bootstrap",
    //   //     showClose:true,
    //   //     timeout:5000
    //   //   }
    //   //)
    //   //}
    //   ) ;
    // }

  }

}

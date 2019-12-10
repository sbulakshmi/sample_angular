import { Vehicle, KeyValuePair, Make } from './../models/vehicle';
import { Component, OnInit } from '@angular/core';
import { VehicleService } from '../services/make.service';
//import { query } from '@angular/core/src/animation/dsl';

@Component({
  selector: 'app-vehicle-list',
  templateUrl: './vehicle-list.component.html',
  styleUrls: ['./vehicle-list.component.css']
})
export class VehicleListComponent implements OnInit {
  private readonly PAGE_SIZE = 3;
vehicles:Vehicle[];
//allVehicles:Vehicle[];
makes: KeyValuePair[];
filter:any={
  pageSize:this.PAGE_SIZE
};
columns=[
  {title:'Id'},
  {title:'Make', sortBy:'make', isSortable:true},
  {title:'Model', sortBy:'model', isSortable:true},
  {title:'Contact', sortBy:'contact', isSortable:true},
  {title:'View'},
]

  constructor(private vehicleService:VehicleService) { }

  ngOnInit() {

    this.getVehicles();
    this.vehicleService.getMakes().subscribe(data=>{

      this.makes= data.map((value,index,array) => {
         return ({id:+value.id, name:value.name});
      });

    })
  }

  private getVehicles()
  {
    this.vehicleService.getVehicles(this.filter).subscribe( data=>{
      //this.allVehicles=
      this.vehicles = data.items;
      debugger;
      this.filter.totalItems = data.totalItems;
    })
  }
  onFilterChange()
  {
    this.filter.page=1;
    this.filter.pageSize=this.PAGE_SIZE;
    this.getVehicles();
    // var vehicles=  this.allVehicles;

    // if(this.filter.makeId)
    // vehicles= vehicles.filter(v=> v.make.id == this.filter.makeId);

    // this.vehicles = vehicles
   // console.log(this.filter.makeId);
  }

  onFilterReset()
  {
    this.filter={};
    this.onFilterChange();
  }

  sortBy(columnName)
  {
    debugger;
    if (this.filter.sortBy===columnName)
      this.filter.isSortAscen=!this.filter.isSortAscen;
    else 
    {
      this.filter.sortBy=columnName;
      this.filter.isSortAscen = true;
    }
    this.getVehicles();
  }

  onPageChanged(page)
  {
    this.filter.pageNum=page;
    this.getVehicles();
  }

}

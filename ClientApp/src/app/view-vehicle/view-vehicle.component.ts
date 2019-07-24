//import { ProgressService } from './../services/progress.service';
import { PhotoService } from './../services/photo.service';
import { Router, ActivatedRoute } from '@angular/router';
import { Vehicle } from './../models/vehicle';
import { ToastyService } from 'ng2-toasty';
import { VehicleService } from './../services/make.service';
import { Component, OnInit, ElementRef, ViewChild, NgZone } from '@angular/core';

@Component({
  selector: 'app-view-vehicle',
  templateUrl: './view-vehicle.component.html',
  styleUrls: ['./view-vehicle.component.css']
})
export class ViewVehicleComponent implements OnInit {
  vehicle: Vehicle;// =  {id :0, make:{id:0, name:''}, features:[], lastUpdated:'', isRegistered:false, model:{id:0, name:''},contact:{name:'',phone:'', email:''}};
  vehicleId: number;
  photos: any[];
  @ViewChild('fileInput') fileInput: ElementRef;
  progress: any;

  constructor(
    private vehicleService: VehicleService,
    private toastyService: ToastyService,
    private route: ActivatedRoute,
    private router: Router,
    private photoService: PhotoService,
   // private progressService: ProgressService,
    private zone: NgZone
  ) {
    route.params.subscribe(p => {
      debugger;
      //this.vehicle.id= +p['id'];

      this.vehicleId = +p['id'];
      if (isNaN(this.vehicleId) || this.vehicleId <= 0) {
        router.navigate(["/vehicles"]);
        return;
      }
    })
  }

  ngOnInit() {
    this.vehicleService.getVehicle(this.vehicleId).subscribe(x => {
      this.vehicle = x;
    })
    this.photoService.getPhotos(this.vehicleId).subscribe(x => {
      this.photos = x;
    })
  }
  editVehicle() {
    this.router.navigate(["/vehicles/edit/" + this.vehicle.id]);
  }

  viewVehicle() {
    this.router.navigate(["/vehicles/"]);
  }
  deleteVehicle() {
    if (confirm("Are you sure?")) {
      this.vehicleService.deleteVehicle(this.vehicle.id).subscribe(x => {
        this.toastyService.success({
          title: "Success",
          msg: "Vehicle removed successfully",
          theme: "bootstrap",
          showClose: true,
          timeout: 5000
        }
        )
        debugger;
        console.log(x);
        this.router.navigate(['/']);
      });
    }
  }
  upload() {

    // this.progressService.startTracking()
    //   .subscribe(progress => {
    //     console.log(progress);
    //     this.zone.run(() => {
    //       console.log(progress);
    //       this.progress = progress;
    //     })
    //   }, null,
    //     () => {
    //       this.progress = null;
    //     }
    //   );
    var nativeElement: HTMLInputElement = this.fileInput.nativeElement;
    var file = nativeElement.files[0];
    nativeElement.value = '';
    //debugger;
    this.photoService.upload(this.vehicle.id, file)
      .subscribe(photo => {
        debugger;
        if(photo.id!== undefined )
        this.photos.push(photo) 
        else
        this.progress = photo;
      
      },
        err => {
          debugger;
          this.toastyService.error({
            title: "Error",
            msg: err.error,
            theme: "bootstrap",
            showClose: true,
            timeout: 5000
          })
        }
      );
  }
}

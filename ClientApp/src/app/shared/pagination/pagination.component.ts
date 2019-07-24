import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-pagination',
  templateUrl: './pagination.component.html',
  styleUrls: ['./pagination.component.css']
})
export class PaginationComponent implements OnInit {
  pages:any[]
  @Input("page-size") pageSize;
  @Input("total-items") totalItems=10;
  @Output("page-changed") pageChanged = new EventEmitter();
  pageCount:number;
  currentPage = 1; 
  constructor() { }

  ngOnInit() {
    
  }

  ngOnChanges()
  {
    debugger;
    this.pageCount =  Math.ceil(this.totalItems/this.pageSize);
    this.pages =[];
    for (var i=1; i<= this.pageCount; i++)
    this.pages.push(i);
  }

  changePage(page)
  {
    this.currentPage=page;
    this.pageChanged.emit(this.currentPage);
  }

  previous(){
    this.currentPage = this.currentPage-1;
    this.pageChanged.emit(this.currentPage);
  }

  next(){
    this.currentPage = this.currentPage+1;
    this.pageChanged.emit(this.currentPage);
  }


}

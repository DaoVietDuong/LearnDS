import { Component, OnInit } from '@angular/core';
import { ApiService } from '../api.service';
import { Product } from '../product';
import { SignalRService } from '../signalR/signalR-service';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.scss']
})
export class ProductsComponent implements OnInit {
  displayedColumns: string[] = ['name', 'price'];
  data: Product[] = [];
  isLoadingResults = true;
  msg: string;
  constructor(private api: ApiService, public signalRService: SignalRService) {

   }

  ngOnInit() {
    this.signalRService.startConnection();
    this.signalRService.ReceiveMessageListener(data => {
      this.msg = data;
    });   

    this.api.getProducts()
    .subscribe((res: any) => {
      this.data = res;
      console.log(this.data);
      this.isLoadingResults = false;
    }, err => {
      console.log(err);
      this.isLoadingResults = false;
    });
  }

}

import { Component, OnInit } from '@angular/core';

import { ApiService } from '../api.service';
import { Etapa } from 'src/model/etapa';

@Component({
  selector: 'app-etapas',
  templateUrl: './etapas.component.html',
  styleUrls: ['./etapas.component.css'],
})
export class EtapasComponent implements OnInit {
  dataSource: any;
  isLoadingResults: boolean;
  count: 0;

  constructor(private api: ApiService) {}

  ngOnInit(): void {
    this.api.getWorkflow().subscribe(
      (res) => {
        this.dataSource = res;
        this.isLoadingResults = false;
      },
      (err) => {
        console.log(err);
        this.isLoadingResults = false;
      }
    );
  }
}

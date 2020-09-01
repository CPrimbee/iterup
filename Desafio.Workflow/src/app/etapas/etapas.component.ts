import { Component, OnInit } from '@angular/core';

import { ApiService } from '../api.service';
import { Etapa } from 'src/model/etapa';
import { Resposta } from 'src/model/resposta';

@Component({
  selector: 'app-etapas',
  templateUrl: './etapas.component.html',
  styleUrls: ['./etapas.component.css'],
})
export class EtapasComponent implements OnInit {
  dataSource: Etapa[];
  isLoadingResults: boolean;

  constructor(private _api: ApiService) {}

  ngOnInit(): void {
    this._api.getEtapas().subscribe(
      (res) => {
        this.dataSource = res;
        console.log(this.dataSource);
        this.isLoadingResults = false;
      },
      (err) => {
        console.log(err);
        this.isLoadingResults = false;
      }
    );
  }
}

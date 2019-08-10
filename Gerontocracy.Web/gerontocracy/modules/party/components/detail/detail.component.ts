import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { PolitikerDetail } from '../../models/politiker-detail';
import { Router } from '@angular/router';

@Component({
  selector: 'app-detail',
  templateUrl: './detail.component.html',
  styleUrls: ['./detail.component.scss', '../../../../geronto-global-styles.scss']
})
export class DetailComponent implements OnInit {

  @Input() data: PolitikerDetail;

  @Output() fullscreen: EventEmitter<boolean> = new EventEmitter<boolean>();
  @Output() close: EventEmitter<any> = new EventEmitter<any>();

  isFullscreen: boolean;

  isLoading: boolean;

  constructor(private router: Router) { }

  displayedColumns = ['id', 'titel', 'erstelltAm', 'erstelltVon', 'showAffairButton'];

  ngOnInit() {
    this.isLoading = false;
  }

  toggleSize(evt: boolean) {
    this.isFullscreen = evt;
    this.fullscreen.emit(this.isFullscreen);
  }

  showAffair(evt: any, id: number) {
    this.isLoading = true;
    this.router.navigate([`affair/top/${id}`]);
  }
}

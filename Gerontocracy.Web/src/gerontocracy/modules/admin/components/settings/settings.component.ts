import { Component, OnInit } from '@angular/core';
import { NewsService } from '../../services/news.service';

@Component({
  selector: 'app-settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.scss']
})
export class SettingsComponent implements OnInit {

  constructor(private newsService: NewsService) { }

  ngOnInit() {
  }

  addSource() {

  }

  removeSource(id: number) {

  }
}

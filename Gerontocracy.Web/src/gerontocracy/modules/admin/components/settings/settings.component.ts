import { Component, OnInit } from '@angular/core';
import { NewsService } from '../../services/news.service';
import { BaseComponent } from '../../../shared/components/base/base.component';
import { MessageService } from 'primeng/api';
import { FormGroup, FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.scss'],
  providers: [MessageService]
})
export class SettingsComponent extends BaseComponent implements OnInit {

  constructor(
    private newsService: NewsService,
    private formBuilder: FormBuilder,
    messageService: MessageService) {
    super(messageService);
  }

  isAdmin: boolean;
  searchForm: FormGroup;

  ngOnInit() {
    this.searchForm = this.formBuilder.group({
      search: ['']
    });
  }

  addSource() {

  }

  removeSource(id: number) {

  }
}

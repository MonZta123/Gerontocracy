import { Component, OnInit, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-nav-top-bar',
  templateUrl: './nav-top-bar.component.html',
  styleUrls: ['./nav-top-bar.component.scss']
})
export class NavTopBarComponent implements OnInit {

  isFullscreen = false;

  @Input() maximized = false;

  @Output() sizeChanged: EventEmitter<boolean> = new EventEmitter<boolean>();
  @Output() close: EventEmitter<void> = new EventEmitter<void>();

  constructor() { }

  ngOnInit(): void {
    this.isFullscreen = this.maximized;
  }

  toggleSize(evt: any) {
    this.isFullscreen = !this.isFullscreen;
    this.sizeChanged.emit(this.isFullscreen);
  }
}

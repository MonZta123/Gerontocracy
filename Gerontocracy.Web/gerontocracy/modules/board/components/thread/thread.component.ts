import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
import { ThreadDetail } from '../../models/thread-detail';
import { Post } from '../../models/post';
import { NestedTreeControl } from '@angular/cdk/tree';
import { BoardService } from '../../services/board.service';

@Component({
  selector: 'app-thread',
  templateUrl: './thread.component.html',
  styleUrls: ['./thread.component.scss', '../../../../geronto-global-styles.scss']
})
export class ThreadComponent implements OnInit {

  @Input() data: ThreadDetail;

  @Output() fullscreen: EventEmitter<boolean> = new EventEmitter<boolean>();
  @Output() close: EventEmitter<any> = new EventEmitter<any>();

  isLoadingData: boolean;
  treeControl: NestedTreeControl<Post>;
  dataSource: Post[];

  isFullscreen: boolean;

  constructor(private boardService: BoardService) {
    this.treeControl = new NestedTreeControl((node: Post) => node.children);
  }

  ngOnInit() {
    this.isFullscreen = false;
    this.dataSource = [this.data.initialPost];
  }

  hasChild(_: number, node: Post) {
    return node.children != null && node.children.length > 0;
  }

  toggleSize(evt: boolean) {
    this.isFullscreen = evt;
    this.fullscreen.emit(this.isFullscreen);
  }

  onRefresh() {
    this.isLoadingData = true;
    this.boardService.getThread(this.data.id).toPromise()
      .then(n => this.data = n)
      .then(() => {
        this.dataSource = null;
        this.dataSource = [this.data.initialPost];
      })
      .then(() => this.isLoadingData = false);
  }
}

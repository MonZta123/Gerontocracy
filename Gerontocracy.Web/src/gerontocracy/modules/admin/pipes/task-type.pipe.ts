import { Pipe, PipeTransform } from '@angular/core';
import { TaskType } from '../models/task-type';

@Pipe({
  name: 'taskType'
})
export class TaskTypePipe implements PipeTransform {

  transform(value: any, ...args: any[]): any {

    switch (value) {
      case TaskType.AffairReport: return 'Vorfall';
      case TaskType.AffairThreadTask: return 'Vorfall-Thread';
      case TaskType.UserReport: return 'User';
      case TaskType.PostReport: return 'Post';
    }

    return null;
  }

}

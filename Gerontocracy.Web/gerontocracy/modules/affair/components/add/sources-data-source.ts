import { DataSource } from '@angular/cdk/table';
import { BehaviorSubject, Observable } from 'rxjs';
import { QuelleData } from './quelle-data';
import { QuelleAdd } from '../../models/quelle-add';

export class SourcesDataSource extends DataSource<QuelleData> {
    constructor() {
        super();
        this.data = new BehaviorSubject<QuelleData[]>([]);
    }

    data: BehaviorSubject<QuelleData[]>;

    add(source: QuelleAdd) {
        const newSource: QuelleData = {
            ...source,
            index: this.data.value.length
        };

        const data = this.data.getValue();
        data.push(newSource);

        this.data.next(data);
    }

    getData = (): QuelleAdd[] =>
        this.data.getValue().map(n => {
            return {
                zusatz: n.zusatz,
                url: n.url
            } as QuelleAdd;
        })

    remove(index: number) {
        const data: QuelleData[] = this.data.getValue();

        data.splice(index, 1);

        let count = 0;
        data.forEach(n => n.index = count++);

        this.data.next(data);
    }

    connect(): Observable<QuelleData[]> {
        return this.data.asObservable();
    }

    disconnect() { }
}

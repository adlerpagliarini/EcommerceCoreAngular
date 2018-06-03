import { Component, Input } from '@angular/core';
import { HttpCommonService } from '../../services/http-common.service';
import { ActivatedRoute } from '@angular/router';
import { FlashMessagesService } from 'angular2-flash-messages';

@Component({
    selector: 'category-list',
    templateUrl: './category-list.component.html',
    providers: [HttpCommonService]
})
/** category-list component*/
export class CategoryListComponent {
    //public strDate:string  = "2010-10-25";
    public apiName: string
    public items: any;
    private sub: any;
    id: number = 0;

    constructor(private _httpCommonService: HttpCommonService,
        private flashMessagesService: FlashMessagesService,
        private route: ActivatedRoute) {
        this.apiName = "admin/categoryNG";
        this.sub = route.params.subscribe(params => {
            _httpCommonService.getList(this.apiName + "/GetAll").subscribe(data => {
                this.items = data
            });
        });
    }
    ngOnDestroy() {
        this.sub.unsubscribe();
    }
}
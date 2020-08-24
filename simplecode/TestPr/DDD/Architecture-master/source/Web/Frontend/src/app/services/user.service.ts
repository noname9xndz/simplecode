import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { GridParametersModel } from "src/app/components/grid/grid-parameters.model";
import { GridService } from "src/app/components/grid/grid.service";
import { UserModel } from "src/app/models/user.model";

@Injectable({ providedIn: "root" })
export class AppUserService {
    private readonly url = "users";

    constructor(
        private readonly http: HttpClient,
        private readonly gridService: GridService) { }

    add(model: UserModel) {
        return this.http.post<number>(this.url, model);
    }

    delete(id: number) {
        return this.http.delete(`${this.url}/${id}`);
    }

    get(id: number) {
        return this.http.get<UserModel>(`${this.url}/${id}`);
    }

    grid(parameters: GridParametersModel) {
        return this.gridService.get<UserModel>(`${this.url}/grid`, parameters);
    }

    inactivate(id: number) {
        return this.http.patch(`${this.url}/${id}/inactivate`, {});
    }

    list() {
        return this.http.get<UserModel[]>(this.url);
    }

    update(model: UserModel) {
        return this.http.put(`${this.url}/${model.id}`, model);
    }
}

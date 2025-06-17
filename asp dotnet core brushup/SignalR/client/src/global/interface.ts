import { AlertColor } from "@mui/material";
import type { AppUserRoles } from "./components/enums/enums";
import { IToasterData } from "./components/toaster/interface";

export interface IGlobalStore{
  appUser: IAppUserDetails,
  setAppUser: (data:IAppUserDetails) => void;
    activeUrl: string,
    setActiveUrl: (path:string) => void;
    toasterData: IToasterData,
    setToasterData: (data:IToasterData) => void;
    closeToaster: () => void;
}

export type IAppUserDetails =  {
  id: number;
  userName: string;
  email: string;
  roles: IRoleDetails[];
  isNew: boolean;
}

export interface IRoleDetails {
  roleName: AppUserRoles;
}

export interface IResponse{
  message: string;
  severity: AlertColor | undefined
}
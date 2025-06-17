import { create } from "zustand";
import { IAppUserDetails, IGlobalStore } from "./interface";
import { IToasterData } from "./components/toaster/interface";


const defaultAppUser = {
    id: 0,
    userName: "",
    email: "",
    roles: [],
    isNew: false,
}

export const useGlobalStore = create<IGlobalStore>((set) => ({
    appUser: defaultAppUser,
    setAppUser: (data: IAppUserDetails) => {
        set({appUser: data})
    },

    activeUrl: "",
    setActiveUrl: (path: string) => set({
        activeUrl: path
    }),
    
    toasterData: {
        message: "",
        severity: undefined,
        open: false
    },
    setToasterData: (data: IToasterData) => {
        set({
            toasterData: data
        })
    },
    closeToaster: () => {
        set({
            toasterData:
            {
                message: "",
                severity: undefined,
                open: false
            }
        })
    }
}))
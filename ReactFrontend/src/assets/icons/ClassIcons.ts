import { Class } from "../../utils/apiModels";
import blade from "./Blade.png";
import cur from "./Cur.png";
import exile from "./Exile.png";
import grovemaiden from "./GroveMaiden.png";
import harbinger from "./Harbinger.png";
import huntress from "./Huntress.png";
import penitent from "./Penitent.png";
import priest from "./Priest.png";
import ranger from "./Ranger.png";
import warbear from "./Warbear.png";
import warden from "./Warden.png";
import witch from "./Witch.png";

export const classIconMap : {[key in Class]: string} = {
    [Class.Blade] : blade,
    [Class.Cur]: cur,
    [Class.Exile]: exile,    
    [Class.GroveMaiden]: grovemaiden,
    [Class.Harbinger]: harbinger,
    [Class.Huntress]: huntress,    
    [Class.Penitent]: penitent,
    [Class.Priest]: priest,
    [Class.Ranger]: ranger,    
    [Class.Warbear]: warbear,
    [Class.Warden]: warden,
    [Class.Witch]: witch
}


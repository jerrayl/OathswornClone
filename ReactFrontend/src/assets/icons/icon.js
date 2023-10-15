import troop from "./troops.png";
import building from "./building.png";
import sword from "./sword.png";
import bricks from "./bricks.png";
import carrot from "./carrot.png";
import heart from "./heart.png";
import eye from "./eye.png";
import eye_h from "./eye-h.png";
import u_arrow from "./u-arrow.png";
import u_arrow_h from "./u-arrow-h.png";
import d_arrow from "./d-arrow.png";
import d_arrow_h from "./d-arrow-h.png";
import l_arrow from "./l-arrow.png";
import l_arrow_h from "./l-arrow-h.png";
import r_arrow from "./r-arrow.png";
import r_arrow_h from "./r-arrow-h.png";
import bricks_d from "./bricks_d.png";
import production_d from "./production_d.png";
import tag_d from "./tag_d.png";
import { BONUS_TYPES } from "../../utils/constants";


const icons = {
    troop: troop,
    building: building,
    eye: eye,
    eye_h: eye_h,
    u_arrow: u_arrow,
    u_arrow_h: u_arrow_h,
    d_arrow: d_arrow,
    d_arrow_h: d_arrow_h,
    l_arrow: l_arrow,
    l_arrow_h: l_arrow_h,
    r_arrow: r_arrow,
    r_arrow_h: r_arrow_h,
    bricks_d: bricks_d,
    tag_d: tag_d,
    production_d: production_d
};

export const BONUS_ICONS = {
    [BONUS_TYPES.ATTACK]: sword,
    [BONUS_TYPES.BUILDING_HEALTH]: bricks,
    [BONUS_TYPES.TROOP_HEALTH]: heart,
    [BONUS_TYPES.FOOD]: carrot,
}

export default icons;
import fire from "./FIRE.png";
import water from "./WATER.png";
import earth from "./EARTH.png";
import air from "./AIR.png";
import food from "./FOOD.png";
import arcana from "./ARCANA.png";
import { ELEMENTS } from "../../utils/constants";

const ELEMENT_ICONS = {
    [ELEMENTS.FIRE]: fire,
    [ELEMENTS.WATER]: water,
    [ELEMENTS.EARTH]: earth,
    [ELEMENTS.AIR]: air,
    [ELEMENTS.FOOD]: food,
    [ELEMENTS.ARCANA]: arcana
};

export default ELEMENT_ICONS;

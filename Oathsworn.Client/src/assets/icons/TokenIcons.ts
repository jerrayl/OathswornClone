import { Token } from "../../utils/apiModels";
import animus from "./Animus.png";
import battleflow from "./Battleflow.png";
import defence from "./Defence.png";
import empower from "./Empower.png";
import redraw from "./Redraw.png";

export const tokenIconMap : {[key in Token]: string} = {
    [Token.Animus] : animus,
    [Token.Battleflow]: battleflow,
    [Token.Defence]: defence,    
    [Token.Empower]: empower,
    [Token.Redraw]: redraw
}


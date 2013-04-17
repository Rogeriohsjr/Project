function SelectAll(cbId, gv) {
    //get reference of GridView control
    var grid = document.getElementById(gv);
    //variable to contain the cell of the grid
    var cell;

    if (grid.rows.length > 0) {
        //loop starts from 1. rows[0] points to the header.
        for (i = 1; i < grid.rows.length; i++) {
            //get the reference of first column
            cell = grid.rows[i].cells[0];

            //loop according to the number of childNodes in the cell
            for (j = 0; j < cell.childNodes.length; j++) {
                //if childNode type is CheckBox                 
                if (cell.childNodes[j].type == "checkbox") {
                    //assign the status of the Select All checkbox to the cell checkbox within the grid
                    cell.childNodes[j].checked = document.getElementById(cbId).checked;
                }
            }
        }
    }
}

function FormataData(oCampo, teclapres) {
    var vr = oCampo.value;
    tam = vr.length;

    if (vr.length >= 10) {
        teclapres.returnValue = false;
        return;
    }

    if (navigator.appName.indexOf('Internet Explorer') > 0) { 
        nTecla = teclapres.keyCode;
        setacampo(oCampo);
        if (isSelected(oCampo)) {
            teclapres.returnValue = true;
            return;
        }
    } else {
        nTecla = teclapres.which;
    }

    if ((nTecla < 48) || (nTecla > 57))
        teclapres.returnValue = false;
    else
        if ((tam == 2) || (tam == 5))
            oCampo.value = vr + '/';
}

function FormataNumero(oCampo, teclapres) {
    if (navigator.appName.indexOf('Internet Explorer') > 0) {
        nTecla = teclapres.keyCode;
    } else {
        nTecla = teclapres.which;
    }

    if ((nTecla < 48) || (nTecla > 57))
        teclapres.returnValue = false;
}

function FormataDecimal(oCampo, teclapres) {
    var tam = oCampo.value.length;
    var virg = oCampo.value.indexOf(',');
    var ie = (typeof window.ActiveXObject != 'undefined');
    if (ie)
        code = event.keyCode;
    else
        code = event.which;

    if ((code == 44) && (tam > 0) && virg < 1) {
        teclapres.returnValue = true;
        return;
    }
    if (((tam - virg) == 3) && (virg > 0)) {
        teclapres.returnValue = false;
        return;
    }
    if ((code < 48) || (code > 57))
        teclapres.returnValue = false;

}

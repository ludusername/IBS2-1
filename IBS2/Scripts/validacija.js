var korisnikID;
var unetKorisnikID;

function posalji() {
    korisnikID = document.getElementById("korisnikID").innerHTML;
    unetKorisnikID = document.getElementById("unetKorisnikID").value;
    if (korisnikID != unetKorisnikID) {
        document.getElementById("labelKorisnikID").innerHTML = "Unesite Vaš korisnički ID";
    }
    else {
        document.getElementById("labelKorisnikID").innerHTML = "";
        document.getElementById("btnPosalji").setAttribute("type", "submit");
    }
}
//java-script validacija nakorisnickom pogledu



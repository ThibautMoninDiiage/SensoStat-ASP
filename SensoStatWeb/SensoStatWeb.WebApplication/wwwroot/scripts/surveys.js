function showPopupDeleteSurvey(id) {
  const deleteButton = new Button("Supprimer", "red", () => deleteSurvey(id)); // If click call delete survey
  const cancelButton = new Button("Annuler", "blue", enableWindow); // If click close the pop up

  var text = "Êtes vous certains de vouloir supprimer la séance.";
  const alerte = new Alert("Confirmation suppression", text, "white", [
    deleteButton,
    cancelButton,
  ]);

  new window.alert(alerte);
}

async function deleteSurvey(id) {
    await fetch(window.location.origin + "/surveys/DeleteSurvey?id=" + id, { method: 'DELETE' })

    window.location.replace(window.location.origin + "/surveys");

    enableWindow();
}

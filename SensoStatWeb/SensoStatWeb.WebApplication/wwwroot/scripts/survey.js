function slist (target) {
    // (A) SET CSS + GET ALL LIST ITEMS
    target.classList.add("slist");
    let items = target.getElementsByTagName("li"), current = null;
  
    // (B) MAKE ITEMS DRAGGABLE + SORTABLE
    for (let i of items) {
      // (B1) ATTACH DRAGGABLE
      i.draggable = true;
      
      // (B2) DRAG START - YELLOW HIGHLIGHT DROPZONES
      i.ondragstart = (ev) => {
        current = i;
        for (let it of items) {
          if (it != current) { it.classList.add("hint"); }
        }
      };
      
      // (B3) DRAG ENTER - RED HIGHLIGHT DROPZONE
      i.ondragenter = (ev) => {
        if (i != current) { i.classList.add("active"); }
      };
  
      // (B4) DRAG LEAVE - REMOVE RED HIGHLIGHT
      i.ondragleave = () => {
        i.classList.remove("active");
      };
  
      // (B5) DRAG END - REMOVE ALL HIGHLIGHTS
      i.ondragend = () => { for (let it of items) {
          it.classList.remove("hint");
          it.classList.remove("active");
      }};
   
      // (B6) DRAG OVER - PREVENT THE DEFAULT "DROP", SO WE CAN DO OUR OWN
      i.ondragover = (evt) => { evt.preventDefault(); };
   
      // (B7) ON DROP - DO SOMETHING
      i.ondrop = (evt) => {
        evt.preventDefault();
        if (i != current) {
          let currentpos = 0, droppedpos = 0;
          for (let it=0; it<items.length; it++) {
            if (current == items[it]) { currentpos = it; }
            if (i == items[it]) { droppedpos = it; }
          }
          if (currentpos < droppedpos) {
            i.parentNode.insertBefore(current, i.nextSibling);
          } else {
            i.parentNode.insertBefore(current, i);
          }
        }
      };
    }
  }
  
      function addInput(name) {
          event.preventDefault();
  
          var ul = document.getElementById('sortlist'); // get the list
  
          var li = document.createElement('li'); // Create parent div
          var input = document.createElement('input'); // Create input
          var i = document.createElement('i'); // create i ``
  
          li.classList.add("roundedOrangeInput"); // Add class to parent div
          li.classList.add("bigInput"); // Add class to parent div
  
          li.setAttribute("draggable", "true");
  
          input.placeholder = "Ajouter une " + name; // Add placeholder in input
  
          i.onclick = deleteInput; // When click on i call deleteInput
          i.classList.add("fas"); // Add trash style to i
          i.classList.add("fa-trash-alt"); // Add trash style to i
  
          li.appendChild(input); // Add input to div
          li.appendChild(i);  // Add i to div
  
  
          ul.appendChild(li); // Add div and child to list

          slist(document.getElementById("sortlist"));
      }
  
      function deleteInput(caller){
          caller.srcElement.parentNode.remove(); // find the parent div and remove it
      }
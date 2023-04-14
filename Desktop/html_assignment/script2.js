//select the form
const form = document.querySelector('form');

//add event listener to form
form.addEventListener('submit',(event) => {
  event.preventDefault(); // prevent form submission
  //fetch data
  const name = document.querySelector('#input1').value;
  const email = document.querySelector('#input2').value;
  const website = document.querySelector('#input3').value;
  const imageLink = document.querySelector('#input4').value;
  const genders=document.getElementsByName("gender");
  let selectedGender;
  for (let i = 0; i < genders.length; i++) {
        if (genders[i].checked) {
          selectedGender = genders[i].value;
          break;
        }
    }
  const skills=document.getElementsByName("skills");
  let selectedSkills=[];
  for (let i = 0; i < skills.length; i++) {
        if (skills[i].checked) {
          selectedSkills.push( skills[i].value);
        }
  }
  
  //select the table body
  const tableBody=document.querySelector('tbody');
  //delete the first row 
  tableBody.deleteRow(0);
  
  //create a new html row element
  const newRow=document.createElement('tr')
  //description cell
  const descriptionColumn=document.createElement('td');
  //image cell
  const imageColumn=document.createElement('td');

  descriptionColumn.innerHTML="<h4>"+name+"</h4><p>"+selectedGender+"<br>"+email+"<br>"+"<a href='" + website + "'>" + website + "</a><br>"+selectedSkills+"<br></p>"
  const img=document.createElement("img");
  //set image attributes
  img.src=imageLink;
  img.alt=name+' image';
  img.width=150;
  img.height=150;

  imageColumn.prepend(img)
  newRow.classList.add('fade-in');
  newRow.append(descriptionColumn)
  newRow.append(imageColumn)
  
  //insert the new row in the table body
  
  tableBody.append(newRow);  
});









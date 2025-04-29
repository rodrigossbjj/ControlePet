document.addEventListener('DOMContentLoaded', function () {
    const pets = {
        '1': {
            name: 'Totó',
            species: 'Cachorro',
            breed: 'Labrador',
            age: '5 anos',
            weight: '28 kg',
            color: 'Dourado',
            diseases: 'Nenhuma',
            deficiencies: 'Nenhuma',
            tutor: 'João Silva',
            tutorPhone: '(11) 98765-4321',
            tutorEmail: 'joao.silva@email.com',
            history: [
                { date: '15/04/2023', type: 'routine', description: 'Consulta de rotina', details: 'Check-up anual, tudo normal' },
                { date: '10/03/2023', type: 'vaccine', description: 'Vacinação', details: 'Vacina antirrábica e V8' }
            ]
        },
        '2': {
            name: 'Mingau',
            species: 'Gato',
            breed: 'Persa',
            age: '3 anos',
            weight: '4.5 kg',
            color: 'Branco',
            diseases: 'Problemas renais',
            deficiencies: 'Nenhuma',
            tutor: 'Maria Souza',
            tutorPhone: '(11) 91234-5678',
            tutorEmail: 'maria.souza@email.com',
            history: [
                { date: '12/04/2023', type: 'routine', description: 'Acompanhamento renal', details: 'Exames de sangue realizados' }
            ]
        }
    };

    window.showPetSelection = function () {
        document.getElementById('pet-selector').style.display = 'block';
        document.getElementById('appointment-form').style.display = 'none';
        document.getElementById('pet-info').innerHTML = `
        <div class="placeholder">
          <i class="fas fa-paw"></i>
          <p>Selecione um pet para visualizar ou editar</p>
        </div>
      `;
    };

    window.loadPetData = function () {
        const petId = document.getElementById('pet-list').value;
        if (!petId) return;
        const pet = pets[petId];
        renderPetInfo(pet);
        document.getElementById('pet-selector').style.display = 'none';
    };

    function renderPetInfo(pet) {
        let historyHTML = pet.history.map(item => `
        <div class="history-item">
          <span class="history-date">${item.date}</span>
          <span class="history-type ${item.type}">${getTypeLabel(item.type)}</span>
          <div>${item.description}</div>
          ${item.details ? `<div class="history-details">${item.details}</div>` : ''}
        </div>
      `).join('');

        document.getElementById('pet-info').innerHTML = `
        <div class="pet-details">
          <div class="detail-group">
            <h4><i class="fas fa-info-circle"></i> Informações Básicas</h4>
            <div class="detail-item">
              <span class="detail-label">Nome:</span>
              <span>${pet.name}</span>
            </div>
            <div class="detail-item">
              <span class="detail-label">Espécie:</span>
              <span>${pet.species}</span>
            </div>
            <div class="detail-item">
              <span class="detail-label">Raça:</span>
              <span>${pet.breed}</span>
            </div>
            <div class="detail-item">
              <span class="detail-label">Idade:</span>
              <span>${pet.age}</span>
            </div>
            <div class="detail-item">
              <span class="detail-label">Peso:</span>
              <span>${pet.weight}</span>
            </div>
            <div class="detail-item">
              <span class="detail-label">Cor:</span>
              <span>${pet.color}</span>
            </div>
          </div>
          
          <div class="detail-group">
            <h4><i class="fas fa-heartbeat"></i> Saúde</h4>
            <div class="detail-item">
              <span class="detail-label">Doenças:</span>
              <span>${pet.diseases}</span>
            </div>
            <div class="detail-item">
              <span class="detail-label">Deficiências:</span>
              <span>${pet.deficiencies}</span>
            </div>
            
            <h4><i class="fas fa-user"></i> Tutor</h4>
            <div class="detail-item">
              <span class="detail-label">Nome:</span>
              <span>${pet.tutor}</span>
            </div>
            <div class="detail-item">
              <span class="detail-label">Telefone:</span>
              <span>${pet.tutorPhone}</span>
            </div>
            <div class="detail-item">
              <span class="detail-label">E-mail:</span>
              <span>${pet.tutorEmail}</span>
            </div>
          </div>
          
          <div class="medical-history">
            <h4><i class="fas fa-file-medical-alt"></i> Histórico Médico</h4>
            ${historyHTML}
          </div>
        </div>
      `;
    }

    window.showAppointmentForm = function () {
        document.getElementById('appointment-form').style.display = 'block';
        document.getElementById('pet-selector').style.display = 'none';
        document.getElementById('pet-info').innerHTML = `
        <div class="placeholder">
          <i class="fas fa-paw"></i>
          <p>Preencha os dados da consulta</p>
        </div>
      `;
    };

    window.showMedicalHistory = function () {
        showPetSelection();
    };

    function getTypeLabel(type) {
        const types = {
            'routine': 'Rotina',
            'vaccine': 'Vacinação',
            'emergency': 'Emergência',
            'surgery': 'Cirurgia'
        };
        return types[type] || type;
    }

    window.showPetForm = function (mode, petId = null) {
        document.getElementById('pet-selector').style.display = 'none';
        document.getElementById('appointment-form').style.display = 'none';

        document.getElementById('pet-info').innerHTML = `
        <div class="form-container">
          <h3><i class="fas fa-plus"></i> ${mode === 'new' ? 'Cadastrar Novo Pet' : 'Editar Pet'}</h3>
          <div class="form-group">
            <label>Nome do Pet:</label>
            <input type="text" class="form-input" id="pet-name">
          </div>
          <div class="form-group">
            <label>Espécie:</label>
            <select class="form-select" id="pet-species">
              <option value="Cachorro">Cachorro</option>
              <option value="Gato">Gato</option>
              <option value="Pássaro">Pássaro</option>
              <option value="Outro">Outro</option>
            </select>
          </div>
          <div class="form-group">
            <label>Raça:</label>
            <input type="text" class="form-input" id="pet-breed">
          </div>
          <div class="form-group">
            <label>Idade:</label>
            <input type="text" class="form-input" id="pet-age">
          </div>
          <div class="form-group">
            <label>Peso:</label>
            <input type="text" class="form-input" id="pet-weight">
          </div>
          <div class="form-group">
            <label>Cor:</label>
            <input type="text" class="form-input" id="pet-color">
          </div>
          <div class="form-group">
            <label>Tutor:</label>
            <select class="form-select" id="pet-tutor">
              <option value="">-- Selecione --</option>
              <option value="João Silva">João Silva</option>
              <option value="Maria Souza">Maria Souza</option>
            </select>
          </div>
          <button class="confirm-btn" onclick="savePet(${mode === 'new' ? 'true' : 'false'})">Salvar</button>
          ${mode === 'edit' ? '<button class="confirm-btn cancel-btn" onclick="showPetSelection()">Cancelar</button>' : ''}
        </div>
      `;

        if (mode === 'edit' && petId) {
            const pet = pets[petId] || {
                name: '',
                species: 'Cachorro',
                breed: '',
                age: '',
                weight: '',
                color: '',
                tutor: ''
            };

            document.getElementById('pet-name').value = pet.name;
            document.getElementById('pet-species').value = pet.species;
            document.getElementById('pet-breed').value = pet.breed;
            document.getElementById('pet-age').value = pet.age;
            document.getElementById('pet-weight').value = pet.weight;
            document.getElementById('pet-color').value = pet.color;
            document.getElementById('pet-tutor').value = pet.tutor;
        }
    };

    window.savePet = function (isNew) {
        const petData = {
            name: document.getElementById('pet-name').value.trim(),
            species: document.getElementById('pet-species').value,
            breed: document.getElementById('pet-breed').value.trim(),
            age: document.getElementById('pet-age').value.trim(),
            weight: document.getElementById('pet-weight').value.trim(),
            color: document.getElementById('pet-color').value.trim(),
            tutor: document.getElementById('pet-tutor').value
        };

        if (!petData.name || !petData.tutor) {
            alert('Por favor, preencha todos os campos obrigatórios');
            return;
        }

        setTimeout(() => {
            alert(`Pet ${isNew ? 'cadastrado' : 'atualizado'} com sucesso!`);
            renderPetInfo({
                ...petData,
                diseases: 'Nenhuma',
                deficiencies: 'Nenhuma',
                tutorPhone: '(11) 98765-4321',
                tutorEmail: 'tutor@email.com',
                history: []
            });
        }, 500);
    };

    window.showTutorForm = function (mode, tutorId = null) {
        document.getElementById('pet-selector').style.display = 'none';
        document.getElementById('appointment-form').style.display = 'none';

        document.getElementById('pet-info').innerHTML = `
        <div class="form-container">
          <h3><i class="fas fa-plus"></i> ${mode === 'new' ? 'Cadastrar Novo Tutor' : 'Editar Tutor'}</h3>
          <div class="form-group">
            <label>Nome Completo:</label>
            <input type="text" class="form-input" id="tutor-name">
          </div>
          <div class="form-group">
            <label>Telefone:</label>
            <input type="text" class="form-input" id="tutor-phone">
          </div>
          <div class="form-group">
            <label>E-mail:</label>
            <input type="email" class="form-input" id="tutor-email">
          </div>
          <button class="confirm-btn" onclick="saveTutor(${mode === 'new' ? 'true' : 'false'})">Salvar</button>
          ${mode === 'edit' ? '<button class="confirm-btn cancel-btn" onclick="showPetSelection()">Cancelar</button>' : ''}
        </div>
      `;

        if (mode === 'edit' && tutorId) {
            const tutor = {
                name: 'João Silva',
                phone: '(11) 98765-4321',
                email: 'joao.silva@email.com'
            };

            document.getElementById('tutor-name').value = tutor.name;
            document.getElementById('tutor-phone').value = tutor.phone;
            document.getElementById('tutor-email').value = tutor.email;
        }
    };

    window.saveTutor = function (isNew) {
        const tutorData = {
            name: document.getElementById('tutor-name').value.trim(),
            phone: document.getElementById('tutor-phone').value.trim(),
            email: document.getElementById('tutor-email').value.trim()
        };

        if (!tutorData.name || !tutorData.phone) {
            alert('Por favor, preencha todos os campos obrigatórios');
            return;
        }

        setTimeout(() => {
            alert(`Tutor ${isNew ? 'cadastrado' : 'atualizado'} com sucesso!`);
            document.getElementById('pet-info').innerHTML = `
          <div class="info-container">
            <h3>Tutor ${isNew ? 'cadastrado' : 'atualizado'} com sucesso!</h3>
            <p>Nome: ${tutorData.name}</p>
            <p>Telefone: ${tutorData.phone}</p>
            <p>E-mail: ${tutorData.email || 'Não informado'}</p>
          </div>
        `;
        }, 500);
    };
});
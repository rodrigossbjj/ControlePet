use ControlePet

CREATE TABLE Tutores (
    tut_Id INT PRIMARY KEY IDENTITY(1,1),
    tut_Nome NVARCHAR(100) NOT NULL,
    tut_Email NVARCHAR(100),
    tut_Telefone NVARCHAR(20),
    tut_DataCadastro DATETIME DEFAULT GETDATE()
)


CREATE TABLE Pets (
    pet_Id INT PRIMARY KEY IDENTITY(1,1),
    pet_Nome NVARCHAR(100) NOT NULL,
    pet_Especie NVARCHAR(50),
    pet_Raca NVARCHAR(50),
    pet_Sexo CHAR(1), -- M ou F
    pet_DataNascimento DATE,
    pet_Castrado BIT,
    pet_Cor NVARCHAR(30),
    pet_Porte VARCHAR(20),
    pet_Observacoes TEXT,
    pet_FotoUrl NVARCHAR(255),
    pet_Ativo BIT DEFAULT 1,
    pet_TutorId INT NOT NULL,
    FOREIGN KEY (pet_TutorId) REFERENCES Tutores(tut_Id)
)

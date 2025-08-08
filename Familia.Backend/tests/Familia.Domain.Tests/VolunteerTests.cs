using Familia.Domain.Aggregates.VolunteerAggregate.AggregateRoot;
using Familia.Domain.Aggregates.VolunteerAggregate.Entities;
using Familia.Domain.Aggregates.VolunteerAggregate.ValueObjects;
using Familia.Domain.Shared.EntityIds;
using Familia.Domain.Shared.ValueObjects;
using FluentAssertions;

namespace Familia.Domain.Tests
{
    public class VolunteerTests
    {
        /// <summary>
        /// Проверяет, что первое животное, которое будет добавлено, будет иметь позицию 1.
        /// </summary>
        /// <remarks>
        /// Ожидается, что животному будет присвоена позиция 1
        /// </remarks>
        [Fact]
        public void Add_First_Pet_Get_First_Position()
        {
            // Arrange
            var volunteer = GetVolunteer();
            var pet = GetPet();

            // Act
            var result = volunteer.AddPet(pet);

            // Assert
            var addedPetResult = volunteer.GetPetById(pet.Id);


            result.IsSuccess.Should().BeTrue();
            addedPetResult.IsSuccess.Should().BeTrue();
            addedPetResult.Value.Id.Should().Be(pet.Id);
            addedPetResult.Value.Position.Should().Be(Position.First);
        }

        /// <summary>
        /// Проверяет корректность присвоения позиций животным при их добавлении к волонтеру.
        /// </summary>
        /// <remarks>
        /// Ожидается, что каждому животному будет присвоена уникальная позиция,
        /// соответствующая порядку добавления (1, 2, 3 и т.д.).
        /// </remarks>
        [Fact]
        public void Add_Pets_Return_Right_Positions()
        {
            // Arrange
            var volunteer = GetVolunteer();
            var pets = Enumerable.Range(1, 5).Select(_ => GetPet()).ToList();

            // Act
            var results = pets.Select(volunteer.AddPet).ToList();

            // Assert
            for (int i = 0; i < pets.Count; i++)
            {
                var pet = pets[i];
                var result = results[i];
                var expectedPosition = Position.Create(i + 1).Value;
                var addedPetResult = volunteer.GetPetById(pet.Id);


                result.IsSuccess.Should().BeTrue();
                addedPetResult.IsSuccess.Should().BeTrue();
                addedPetResult.Value.Id.Should().Be(pet.Id);
                addedPetResult.Value.Position.Should().Be(expectedPosition);
            }
        }

        /// <summary>
        /// Проверяет, что при попытке поменять местами животное, ничего не происходит.
        /// </summary>
        /// <remarks>
        /// Ожидается, что животное будет иметь тот же самый номер.
        /// </remarks>
        [Fact]
        public void Move_Pet_To_Same_Position_Does_Nothing()
        {
            // Arrange
            var volunteer = GetVolunteer();
            var pets = Enumerable.Range(1, 5).Select(_ => GetPet()).ToList();
            var firstPosition = Position.First;
            pets.Select(volunteer.AddPet).ToList();

            // Act
            var result = volunteer.MovePet(pets.First(), firstPosition);

            // Assert
            result.IsSuccess.Should().BeTrue();
            volunteer.Pets.First(p => p.Position == Position.First);
            pets[0].Position.Value.Should().Be(firstPosition.Value);
        }

        /// <summary>
        /// Проверяет, что при поменять позицию у животного на первую, 
        /// произойдет смена позиции.
        /// </summary>
        /// <remarks>
        /// Ожидается, что животное будет иметь первый номер.
        /// </remarks>
        [Fact]
        public void Move_Pet_To_First_Position_Is_Success()
        {
            // Arrange
            var volunteer = GetVolunteer();
            var pets = Enumerable.Range(1, 5).Select(_ => GetPet()).ToList();
            var firstPosition = Position.First;
            pets.Select(volunteer.AddPet).ToList();

            // Act
            var result = volunteer.MovePetToFirstPosition(pets[4]);

            // Assert
            result.IsSuccess.Should().BeTrue();
            pets[4].Position.Value.Should().Be(firstPosition.Value);
        }

        /// <summary>
        /// Проверяет, что при поменять позицию у животного на последнюю, 
        /// произойдет смена позиции.
        /// </summary>
        /// <remarks>
        /// Ожидается, что животное будет иметь последний номер.
        /// </remarks>
        [Fact]
        public void Move_Pet_To_Last_Position_Is_Success()
        {
            // Arrange
            var volunteer = GetVolunteer();
            var pets = Enumerable.Range(1, 5).Select(_ => GetPet()).ToList();
            var lastPosition = Position.Create(pets.Count()).Value;
            pets.Select(volunteer.AddPet).ToList();

            // Act
            var result = volunteer.MovePetToLastPosition(pets[0]);

            // Assert
            result.IsSuccess.Should().BeTrue();
            pets[0].Position.Value.Should().Be(lastPosition.Value);
        }


        private static Volunteer GetVolunteer()
        {
            var fullName = FullName.Create("Test", "Test", "Test").Value;
            var contactPhone = ContactPhone.Create("+79502557325").Value;
            var helpRequisites = HelpRequisites.Create("test", "test").Value;
            var socialMedia = SocialMedia.Create("test", "test").Value;
            var volunteer = new Volunteer(
                VolunteerId.NewVolunteerId(),
                fullName,
                "test",
                "test",
                1,
                contactPhone,
                helpRequisites,
                [socialMedia]);

            return volunteer;
        }

        private static Pet GetPet()
        {
            var address = Address.Create("test", "test", "test", "test").Value;
            var bodyMeasurements = BodyMeasurements.Create(22, 22).Value;
            var contactPhone = ContactPhone.Create("+79502557325").Value;
            var helpRequisites = HelpRequisites.Create("test", "test").Value;
            var birth = DateTime.UtcNow;
            var creationDate = DateTime.UtcNow;
            var helpStatus = HelpStatus.Create("Ищет дом").Value;
            var petId = PetId.NewPetId();

            var pet = new Pet(
                petId,
                null,
                "test",
                "test",
                "test",
                "test",
                address,
                bodyMeasurements,
                contactPhone,
                helpRequisites,
                true,
                birth,
                true,
                helpStatus,
                creationDate);

            return pet;
        }
    }
}

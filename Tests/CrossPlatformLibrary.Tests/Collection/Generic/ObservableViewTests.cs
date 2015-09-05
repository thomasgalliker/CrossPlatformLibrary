﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using CrossPlatformLibrary.Collection.Generic;

using FluentAssertions;

using Xunit;

namespace CrossPlatformLibrary.Tests.Collection.Generic
{
    public class ObservableViewTests
    {
        private const string FilteredStringItemA = "A";
        private const string FilteredStringItemB = "B";
        private const string FilteredStringItemC = "C";

        #region Filtering
        [Fact]
        public void ShouldRetrieveAnUnfilteredView()
        {
            // Arrange
            var stringList = new List<string>
            {
                FilteredStringItemA, 
                FilteredStringItemB, 
                FilteredStringItemC
            };
            var observableStringView = new ObservableView<string>(stringList);

            // Act
            ObservableCollection<string> unfilteredView = observableStringView.View;

            // Assert
            unfilteredView.Should().NotBeNull();
            unfilteredView.Should().HaveCount(3);
            unfilteredView[0].Should().Be(FilteredStringItemA);
            unfilteredView[1].Should().Be(FilteredStringItemB);
            unfilteredView[2].Should().Be(FilteredStringItemC);

            foreach (var sourceItem in observableStringView.Source)
            {
                var sourceitemIsContained = unfilteredView.Contains(sourceItem);
                sourceitemIsContained.Should().BeTrue();
            }

            foreach (var item in unfilteredView)
            {
                var sourceitemIsContained = observableStringView.Source.Contains(item);
                sourceitemIsContained.Should().BeTrue();
            }
        }

        [Fact]
        public void ShouldTestFilterHandlerWithNoFilterApplied()
        {
            // Arrange
            var stringList = new List<string>
            {
                FilteredStringItemA, 
                FilteredStringItemB, 
                FilteredStringItemC
            };
            var observableStringView = new ObservableView<string>(stringList);
            observableStringView.FilterHandler += (sender, e) => { };

            // Act
            ObservableCollection<string> filteredView = observableStringView.View;

            // Assert
            filteredView.Should().NotBeNull();
            filteredView.Should().HaveCount(3);
            filteredView[0].Should().Be(FilteredStringItemA);
            filteredView[1].Should().Be(FilteredStringItemB);
            filteredView[2].Should().Be(FilteredStringItemC);
        }

        [Fact]
        public void ShouldTestFilterHandlerWithDefinedFilterCriteria()
        {
            // Arrange
            var carsList = new ObservableCollection<Car>
            {
                this.carAudiA1, 
                this.carAudiA3, 
                this.carBmwM1, 
                this.carBmwM3, 
                this.carVwPolo,
                this.carVwGolf
            };

            var observableCarsView = new ObservableView<Car>(carsList);
            observableCarsView.FilterHandler += (sender, e) => e.IsAllowed = e.Item.Brand == CarBrand.BMW;

            // Act
            ObservableCollection<Car> filteredView = observableCarsView.View;

            // Assert
            filteredView.Should().NotBeNull();
            filteredView.Should().HaveCount(2);
            filteredView.Single(x => x.Model == this.carBmwM1.Model).Should().NotBeNull();
            filteredView.Single(x => x.Model == this.carBmwM3.Model).Should().NotBeNull();
        }

        [Fact]
        public void ShouldRaisePropertyChangedEventIfNewItemsAreAdded()
        {
            // Arrange
            var receivedEvents = new List<string>();
            var carsList = new ObservableCollection<Car>
            {
                this.carAudiA1, 
                this.carAudiA3, 
                this.carBmwM1, 
                this.carBmwM3, 
                this.carVwPolo,
                this.carVwGolf
            };

            var observableCarsView = new ObservableView<Car>(carsList);
            observableCarsView.FilterHandler += (sender, e) => e.IsAllowed = e.Item.Brand == CarBrand.BMW; ;
            observableCarsView.PropertyChanged += (sender, e) => receivedEvents.Add(e.PropertyName);

            // Act
            // Let's add 3 new cars. One of them is a BMW which is 'allowed' by the filter criteria.
            // So, all 3 cards should raise a Source PropertyChanged event - but only the BMW should raise a View PropertyChanged event.
            carsList.Add(this.carAudiA4);
            carsList.Add(this.carBmwX5);
            carsList.Add(this.carVwGolfGti);

            ObservableCollection<Car> filteredView = observableCarsView.View;

            // Assert
            filteredView.Should().NotBeNull();
            filteredView.Should().HaveCount(3);
            filteredView.Single(x => x.Model == this.carBmwM1.Model).Should().NotBeNull();
            filteredView.Single(x => x.Model == this.carBmwM3.Model).Should().NotBeNull();
            filteredView.Single(x => x.Model == this.carBmwX5.Model).Should().NotBeNull();

            receivedEvents.Should().HaveCount(9);
            receivedEvents.Count(x => x == "Source").Should().Be(3);
            receivedEvents.Count(x => x == "View").Should().Be(3);
            receivedEvents.Count(x => x == "Groups").Should().Be(3);
        }

        [Fact]
        public void ShouldRaisePropertyChangedEventIfItemsAreRemoved()
        {
            // Arrange
            var receivedEvents = new List<string>();
            var carsList = new ObservableCollection<Car>
            {
                this.carAudiA1, 
                this.carAudiA3, 
                this.carBmwM1, 
                this.carBmwM3, 
                this.carVwPolo,
                this.carVwGolf
            };

            var observableCarsView = new ObservableView<Car>(carsList);
            observableCarsView.FilterHandler += (sender, e) => e.IsAllowed = e.Item.Brand == CarBrand.BMW; ;
            observableCarsView.PropertyChanged += (sender, e) => receivedEvents.Add(e.PropertyName);

            // Act
            // Let's remove 3 existing cars. The View should then only contain the remaining BMW (M3).
            carsList.Remove(this.carBmwM1);
            carsList.Remove(this.carVwPolo);
            carsList.Remove(this.carAudiA1);

            ObservableCollection<Car> filteredView = observableCarsView.View;

            // Assert
            filteredView.Should().NotBeNull();
            filteredView.Should().HaveCount(1);
            filteredView.Single(x => x.Model == this.carBmwM3.Model).Should().NotBeNull();

            receivedEvents.Should().HaveCount(9);
            receivedEvents.Count(x => x == "Source").Should().Be(3);    // 3 Source changes, because we removed 3 new elements
            receivedEvents.Count(x => x == "View").Should().Be(3);   
            receivedEvents.Count(x => x == "Groups").Should().Be(3); 
        }
        #endregion

        #region Searching
        [Fact]
        public void ShouldFindItemsOnSearchText()
        {
            // Arrange
            var carsList = new ObservableCollection<Car>
            {
                this.carAudiA1, 
                this.carAudiA3, 
                this.carBmwM1, 
                this.carBmwM3, 
                this.carVwPolo,
                this.carVwGolf
            };

            var observableCarsView = new ObservableView<Car>(carsList);

            // Act
            observableCarsView.SearchText = "Polo";

            // Assert
            var searchView = observableCarsView.View;
            searchView.Should().NotBeNull();
            searchView.Should().HaveCount(1);

            searchView.Single(x => x.Model == this.carVwPolo.Model).Should().NotBeNull();
        }

        [Fact]
        public void ShouldResetSearchText()
        {
            // Arrange
            var carsList = new ObservableCollection<Car>
            {
                this.carAudiA1, 
                this.carAudiA3, 
                this.carBmwM1, 
                this.carBmwM3, 
                this.carVwPolo,
                this.carVwGolf
            };

            var observableCarsView = new ObservableView<Car>(carsList);
            observableCarsView.SearchText = "Polo";

            // Act
            observableCarsView.SearchText = "";

            // Assert
            var searchView = observableCarsView.View;
            searchView.Should().NotBeNull();
            searchView.Should().HaveCount(carsList.Count);
        }
        #endregion

        #region Grouping
        [Fact]
        public void ShouldUseAlphaGroupKeyAlgorithmByDefaultToGenerateGroupKeys()
        {
            // Arrange
            var carsList = new ObservableCollection<Car>
            {
                this.carAudiA1, 
                this.carAudiA3, 
                this.carBmwM1, 
                this.carBmwM3, 
                this.carVwPolo,
                this.carVwGolf
            };

            var observableCarsView = new ObservableView<Car>(carsList);
            observableCarsView.GroupKey = car => car.Brand.ToString();

            // Act
            var groups = observableCarsView.Groups;

            // Assert
            groups.Should().NotBeNull();
            groups.Should().HaveCount(3);

            var groupAudi = groups.Single(g => g.Key == "a");
            groupAudi.Should().NotBeNull("AlphaGroupKeyAlgorithm should generate 'a' with the CarBrand.Audi.ToString()");
            groupAudi.Should().HaveCount(2);

            var groupBMW = groups.SingleOrDefault(g => g.Key == "b");
            groupBMW.Should().NotBeNull("AlphaGroupKeyAlgorithm should generate 'b' with the CarBrand.BMW.ToString()");
            groupBMW.Should().HaveCount(2);

            var groupVW = groups.Single(g => g.Key == "v");
            groupVW.Should().NotBeNull("AlphaGroupKeyAlgorithm should generate 'v' with the CarBrand.VW.ToString()");
            groupVW.Should().HaveCount(2);
        }
        #endregion

        #region Sorting
        [Fact]
        public void ShouldOrderSingleOrderSpecificationDescending()
        {
            // Arrange
            var carsList = new ObservableCollection<Car>
            {
                this.carAudiA1, 
                this.carAudiA3, 
                this.carBmwM1, 
                this.carBmwM3, 
                this.carVwPolo,
                this.carVwGolf
            };

            var observableCarsView = new ObservableView<Car>(carsList);
            observableCarsView.AddOrderSpecification(new OrderSpecification<Car>(x => x.Brand, OrderDirection.Descending));

            // Act
            var orderedView = observableCarsView.View;

            // Assert
            orderedView.Should().NotBeNull();
            orderedView.Should().HaveCount(carsList.Count);

            orderedView[0].Brand.Should().Be(this.carVwPolo.Brand); // "Frist item in the View should be " + this.carVwPolo.Model
            orderedView[0].Model.Should().Be(this.carVwPolo.Model);
            orderedView[5].Brand.Should().Be(this.carAudiA1.Brand); // "Last item in the View should be " + this.carAudiA1.Model
            orderedView[5].Brand.Should().Be(this.carAudiA1.Brand);
        }

        [Fact]
        public void ShouldOrderMultipleOrderSpecifications()
        {
            // Arrange
            var carsList = new ObservableCollection<Car>
            {
                this.carAudiA1, 
                this.carAudiA3, 
                this.carBmwM1, 
                this.carBmwM3, 
                this.carVwPolo,
                this.carVwGolf
            };

            var observableCarsView = new ObservableView<Car>(carsList);
            observableCarsView.AddOrderSpecification(
                new OrderSpecification<Car>(x => x.Brand, OrderDirection.Ascending),
                new OrderSpecification<Car>(x => x.Model, OrderDirection.Ascending));

            // Act
            var orderedView = observableCarsView.View;

            // Assert
            orderedView.Should().NotBeNull();
            orderedView.Should().HaveCount(carsList.Count);

            orderedView[0].Model.Should().Be(this.carAudiA1.Model);
            orderedView[1].Model.Should().Be(this.carAudiA3.Model);
            orderedView[2].Model.Should().Be(this.carBmwM1.Model);
            orderedView[3].Model.Should().Be(this.carBmwM3.Model);
            orderedView[4].Model.Should().Be(this.carVwGolf.Model);
            orderedView[5].Model.Should().Be(this.carVwPolo.Model);
        }
        #endregion

        #region Test Classes
        // Predefined car pool
        private readonly Car carAudiA1 = new Car(CarBrand.Audi, "A1");
        private readonly Car carAudiA4 = new Car(CarBrand.Audi, "A4 Avant");
        private readonly Car carAudiA3 = new Car(CarBrand.Audi, "A3 Sportback");
        private readonly Car carBmwM1 = new Car(CarBrand.BMW, "M1");
        private readonly Car carBmwM3 = new Car(CarBrand.BMW, "M3");
        private readonly Car carBmwX5 = new Car(CarBrand.BMW, "X5");
        private readonly Car carVwPolo = new Car(CarBrand.VW, "Polo 1.4 TDI");
        private readonly Car carVwGolf = new Car(CarBrand.VW, "Golf 1.6");
        private readonly Car carVwGolfGti = new Car(CarBrand.VW, "Golf GTI 2.0");

        private enum CarBrand
        {
            Audi,
            BMW,
            VW
        }

        private class Car
        {
            public Car(CarBrand carBrand, string carModel)
            {
                this.Brand = carBrand;
                this.Model = carModel;
            }

            public CarBrand Brand { get; private set; }

            [Searchable]
            public string Model { get; private set; }
        }
        #endregion
    }
}

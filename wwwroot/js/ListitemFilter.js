_search = {
    listItems: $(".filtered-list-wrapper #list").find("li"),

    // Listens for key press activity on search box and fires the search handler
    initializeSearch: function () {
        var searchQuery = $("#search-input");

        // Perform search after key is pressed
        searchQuery.keyup(() => {
            // If input length is greater than or equal to two, then perform search
            if (searchQuery.val().length > 2) {
                _search.handleSearch(searchQuery.val());
            } else {
                _search.clearSearch();
            }
        });
    },
    handleSearch: function (searchTerm) {
        var titles = [];
        _search.listItems.each(function () {
            
            var title = $(this).find("h2").text();
            titles.push(title);
        });

        const options = {
            includeScore: true,
        };

        const fuse = new Fuse(titles, options);
        const results = fuse.search(searchTerm);
        _search.updateResultsUi(results);
    },

    updateResultsUi: function (results) {
        var List = $(".filtered-list-wrapper #list");        
        var itemTitles = {};

        // Detect all titles
        _search.listItems.each(function () {            
            var title = $(this).find("h2").text();           
            itemTitles[title] = $(this);
        });

       
        var matchingParents = [];        
        for (var i = 0; i < results.length; i++) {
           
            var matchTitle = results[i]["item"];

            // If a match is found in the map, add its parent li element to the matchingParents array
            if (itemTitles[matchTitle]) {
                matchingParents.push(itemTitles[matchTitle]);
            }
        }
        _search.listItems
            //.removeClass("pagination-d-none")
            .addClass("search-d-none");

        matchingParents.forEach(function (parent) {
            parent.removeClass("search-d-none");
        });
    },

    clearSearch: function () {
        var List = $(".filtered-list-wrapper #list");
        var items = List.find("li");
        items.removeClass("d-none");
    },
};

_typeFilter = {
    filterResults: function () {

        var typesMenu = $("#categories-filter");
        var types = typesMenu.find('input[type="checkbox"]');
        var list = $(".filtered-list-wrapper #list");
        var allTypes = getAllTypes();
        var selectedTypes = [];

        // Fires whenever a type button was clicked
        types.on("click", function () {
            var type = $(this);
            var selectedType = type.attr("id");

            // If this buttons was already checked...
            if (type.hasClass("checked")) {

                type.removeClass("checked");
                    selectedTypes = jQuery.grep(selectedTypes, function (value) {
                        return value != selectedType;
                });
            }
            // If this wasn't already checked...
            else {
                    type.addClass("checked");
                    type.prop("checked", true);
                    selectedTypes.push(selectedType);
            }

            // Now, firstly lets hide & unpaginate all itmes
            list.find("li").addClass("type-d-none");

            // And now, as long as we have some types selected
            if (selectedTypes.length) {

                // We will filter the visible items based on the types stored in our array
                list.find("li")
                    .filter(function (index) {
                        return jQuery.inArray($(this).data("type"), selectedTypes) !== -1;
                    })
                    .removeClass("type-d-none");

                // But, if there are no types selected/stored in our array, lets show all items
            } else {
                list.find("li").removeClass("type-d-none");
            }
        });

        /***
         * Will get all available types from the filter buttons into an array */
        function getAllTypes() {
            var allTypes = [];
            types.each(function () {
                allTypes.push($(this).attr("id"));
            });

            return allTypes;
        }
    },
    clearFilter: function () {
        var list = $(".filtered-list-wrapper #list");
        var items = list.find("li");
        items.removeClass("type-d-none");
    },
};

_sortByFilter = {
    sortByFilterResults: function () {

        var typeFilter = $("#sortByFilter").val();
        var list = $(".filtered-list-wrapper #list");
        var listItems = list.children("li");

        // Sort list items based on the selected filter type
        if (typeFilter === "1") {
            listItems.sort(function (a, b) {
                // sorted by highest salary
                return parseInt($(b).data("salary"))  - parseInt($(a).data("salary"));
            });
        }
        else if (typeFilter === "2") {
            listItems.sort(function (a, b) {
                // sorted by latest date
                return parseInt($(a).data("date").replace(/\D/g, ''), 10)  - parseInt($(b).data("date").replace(/\D/g, ''), 10);
            });
        }
        // Update the list with sorted items
        list.empty().append(listItems);
    },
};


// Initalizers
$(document).ready(function () {

    _search.initializeSearch();
    _typeFilter.filterResults();
    _sortByFilter.sortByFilterResults();

    // Add event listener to document for change events on #sortByFilter
    $(document).on("change", "#sortByFilter", function () {
        _sortByFilter.sortByFilterResults();
    });
});
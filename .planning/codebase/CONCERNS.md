# Concerns - Sistema La Casa de los Niños

## Technical Debt
- **Low-Level Form Handling**: Overriding `WndProc` for borderless behavior is sensitive to Windows version changes and can be complex to maintain.
- **Lack of Automated Tests**: New features are at risk of breaking existing logic without a regression suite.
- **Dapper Mapping**: Manual mapping or specific SQL queries in repositories might become hard to maintain if the schema grows significantly.

## Potential Bottlenecks
- **Theme Engine Complexity**: As more complex or owner-drawn controls are added, the recursive theme application might hit performance issues or edge cases.
- **SQLite Concurrency**: Standard SQLite might face issues if multi-user scenarios are required in the future (though currently designed for local use).

## UX Considerations
- **Resolution Sensitivity**: The fixed-width sidebar toggle (collapsed/expanded) needs to be tested on ultra-low or ultra-high resolutions.
- **Masked Input**: Initial feedback suggested improving input validation (e.g., phone numbers) which is a work in progress.

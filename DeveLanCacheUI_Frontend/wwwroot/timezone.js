export function getBrowserTimeZone() {
    console.log("HALLOOOO123");
    const options = Intl.DateTimeFormat().resolvedOptions();
    return options.timeZone;
}

/**
 * Returns something like "dd/MM/yyyy, HH:mm:ss"
 * exactly matching the user's default locale style.
 */
export function getUserDateTimePattern() {
    // The 4 for the month is actually 5because months are 0-indexed in JS Date. WHYYYYYYYYY...
    const sample = new Date(6666, 4, 4, 3, 2, 1);

    // Let the browser format it with *no* options
    const raw = sample.toLocaleString();   // user’s own settings

    // If raw contains AM or PM (case insensitive), we assume a 12-hour clock
    const containsAmPm = /AM|PM/i.test(raw);

    // Map concrete values -> symbolic tokens
    const map = {
        '6666': 'yyyy', '66': 'yy',
        '05': 'MM', '5': 'M',
        '04': 'dd', '4': 'd',
        '03': containsAmPm ? 'hh' : 'HH', '3': containsAmPm ? 'h' : 'H', // covers 12-h clocks as well
        '02': 'mm', '2': 'm',
        '01': 'ss', '1': 's'
    };
    // Replace each numeric fragment with its token
    let pattern = raw;
    for (const [num, token] of Object.entries(map)) {
        // pad with word boundaries so we don't touch surrounding punctuation
        pattern = pattern.replace(new RegExp(`\\b${num}\\b`, 'g'), token);
    }

    pattern = pattern.replace(/\b(?:AM|PM)\b/i, 'tt');

    // Tidy: collapse duplicate whitespace
    pattern.replace(/\s+/g, ' ');

    return pattern;
}
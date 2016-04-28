USE [GitHubExtension];
Go
MERGE INTO Categories AS Target 
USING (VALUES 
        (1, 'Bug Reports'),
		(2, 'New Features,improvements and changes'),
		(3, 'Bug Reports, as well as proposals for new features, improvements and changes')
) 
AS Source (ID, Description) 
ON Target.ID = Source.ID 
WHEN NOT MATCHED BY TARGET THEN 
INSERT (Description) 
VALUES (Description);
GO
MERGE INTO Templates AS Target 
USING (VALUES 
        (1, '<!--- Provide a general summary of the issue in the Title above -->

## Context
<!--- Provide a more detailed introduction to the issue itself, and why you consider it to be a bug -->

## Expected Behavior
<!--- Tell us what should happen -->

## Actual Behavior
<!--- Tell us what happens instead -->

## Possible Fix
<!--- Not obligatory, but suggest a fix or reason for the bug -->

## Steps to Reproduce
<!--- Provide a link to a live example, or an unambiguous set of steps to -->
<!--- reproduce this bug include code to reproduce, if relevant -->
1.
2.
3.
4.

## Context
<!--- How has this bug affected you? What were you trying to accomplish? -->

## Your Environment
<!--- Include as many relevant details about the environment you experienced the bug in -->
* Version used:
* Browser Name and version:
* Operating System and version (desktop or mobile):
* Link to your project:
', 'Issue', 1, 'My project runs in the browser'), 
        (2, '<!--- Provide a general summary of the issue in the Title above -->

## Context
<!--- Provide a more detailed introduction to the issue itself, and why you consider it to be a bug -->

## Expected Behavior
<!--- Tell us what should happen -->

## Actual Behavior
<!--- Tell us what happens instead -->

## Possible Fix
<!--- Not obligatory, but suggest a fix or reason for the bug -->

## Steps to Reproduce
<!--- Provide a link to a live example, or an unambiguous set of steps to -->
<!--- reproduce this bug include code to reproduce, if relevant -->
1.
2.
3.
4.

## Context
<!--- How has this bug affected you? What were you trying to accomplish? -->

## Your Environment
<!--- Include as many relevant details about the environment you experienced the bug in -->
* Version used:
* Environment name and version (e.g. PHP 5.4 on nginx 1.9.1):
* Server type and version:
* Operating System and version:
* Link to your project:
','Issue', 1, 'My project all about the server'), 
        (3, '<!--- Provide a general summary of the issue in the Title above -->

## Context
<!--- Provide a more detailed introduction to the issue itself, and why you consider it to be a bug -->

## Expected Behavior
<!--- Tell us what should happen -->

## Actual Behavior
<!--- Tell us what happens instead -->

## Possible Fix
<!--- Not obligatory, but suggest a fix or reason for the bug -->

## Steps to Reproduce
<!--- Provide a link to a live example, or an unambiguous set of steps to -->
<!--- reproduce this bug include code to reproduce, if relevant -->
1.
2.
3.
4.

## Context
<!--- How has this bug affected you? What were you trying to accomplish? -->

## Your Environment
<!--- Include as many relevant details about the environment you experienced the bug in -->
* Version used:
* Environment name and version (e.g. Chrome 39, node.js 5.4):
* Operating System and version (desktop or mobile):
* Link to your project:
','Issue', 1, 'My code runs equally well on the server, as well as the browser'),
		(4, '<!--- Provide a general summary of the issue in the Title above -->

## Detailed Description
<!--- Provide a detailed description of the change or addition you are proposing -->

## Context
<!--- Why is this change important to you? How would you use it? -->
<!--- How can it benefit other users? -->

## Possible Implementation
<!--- Not obligatory, but suggest an idea for implementing addition or change -->

## Your Environment
<!--- Include as many relevant details about the environment you experienced the bug in -->
* Version used:
* Browser Name and version:
* Operating System and version (desktop or mobile):
* Link to your project:
','Issue', 2, 'My project runs in the browser'),
		(5, '<!--- Provide a general summary of the issue in the Title above -->

## Detailed Description
<!--- Provide a detailed description of the change or addition you are proposing -->

## Context
<!--- Why is this change important to you? How would you use it? -->
<!--- How can it benefit other users? -->

## Possible Implementation
<!--- Not obligatory, but suggest an idea for implementing addition or change -->

## Your Environment
<!--- Include as many relevant details about the environment you experienced the bug in -->
* Version used:
* Environment name and version (e.g. PHP 5.4 on nginx 1.9.1):
* Server type and version:
* Operating System and version:
* Link to your project:
','Issue', 2, 'My project all about the server'),
		(6, '<!--- Provide a general summary of the issue in the Title above -->

## Detailed Description
<!--- Provide a detailed description of the change or addition you are proposing -->

## Context
<!--- Why is this change important to you? How would you use it? -->
<!--- How can it benefit other users? -->

## Possible Implementation
<!--- Not obligatory, but suggest an idea for implementing addition or change -->

## Your Environment
<!--- Include as many relevant details about the environment you experienced the bug in -->
* Version used:
* Environment name and version (e.g. Chrome 39, node.js 5.4):
* Operating System and version (desktop or mobile):
* Link to your project:
','Issue', 2, 'My code runs equally well on the server, as well as the browser'),
		(7, '<!--- Provide a general summary of the issue in the Title above -->

## Expected Behavior
<!--- If you describing a bug, tell us what should happen -->
<!--- If you suggesting a change/improvement, tell us how it should work -->

## Current Behavior
<!--- If describing a bug, tell us what happens instead of the expected behavior -->
<!--- If suggesting a change/improvement, explain the difference from current behavior -->

## Possible Solution
<!--- Not obligatory, but suggest a fix/reason for the bug, -->
<!--- or ideas how to implement the addition or change -->

## Steps to Reproduce (for bugs)
<!--- Provide a link to a live example, or an unambiguous set of steps to -->
<!--- reproduce this bug. Include code to reproduce, if relevant -->
1.
2.
3.
4.

## Context
<!--- How has this issue affected you? What are you trying to accomplish? -->
<!--- Providing context helps us come up with a solution that is most useful in the real world -->

## Your Environment
<!--- Include as many relevant details about the environment you experienced the bug in -->
* Version used:
* Browser Name and version:
* Operating System and version (desktop or mobile):
* Link to your project:
','Issue', 3, 'My project runs in the browser'),
		(8, '<!--- Provide a general summary of the issue in the Title above -->

## Expected Behavior
<!--- If youre describing a bug, tell us what should happen -->
<!--- If youre suggesting a change/improvement, tell us how it should work -->

## Current Behavior
<!--- If describing a bug, tell us what happens instead of the expected behavior -->
<!--- If suggesting a change/improvement, explain the difference from current behavior -->

## Possible Solution
<!--- Not obligatory, but suggest a fix/reason for the bug, -->
<!--- or ideas how to implement the addition or change -->

## Steps to Reproduce (for bugs)
<!--- Provide a link to a live example, or an unambiguous set of steps to -->
<!--- reproduce this bug. Include code to reproduce, if relevant -->
1.
2.
3.
4.

## Context
<!--- How has this issue affected you? What are you trying to accomplish? -->
<!--- Providing context helps us come up with a solution that is most useful in the real world -->

## Your Environment
<!--- Include as many relevant details about the environment you experienced the bug in -->
* Version used:
* Environment name and version (e.g. PHP 5.4 on nginx 1.9.1):
* Server type and version:
* Operating System and version:
* Link to your project:
','Issue', 3, 'My project all about the server'),
		(9, '<!--- Provide a general summary of the issue in the Title above -->

## Expected Behavior
<!--- If youre describing a bug, tell us what should happen -->
<!--- If youre suggesting a change/improvement, tell us how it should work -->

## Current Behavior
<!--- If describing a bug, tell us what happens instead of the expected behavior -->
<!--- If suggesting a change/improvement, explain the difference from current behavior -->

## Possible Solution
<!--- Not obligatory, but suggest a fix/reason for the bug, -->
<!--- or ideas how to implement the addition or change -->

## Steps to Reproduce (for bugs)
<!--- Provide a link to a live example, or an unambiguous set of steps to -->
<!--- reproduce this bug. Include code to reproduce, if relevant -->
1.
2.
3.
4.

## Context
<!--- How has this issue affected you? What are you trying to accomplish? -->
<!--- Providing context helps us come up with a solution that is most useful in the real world -->

## Your Environment
<!--- Include as many relevant details about the environment you experienced the bug in -->
* Version used:
* Environment name and version (e.g. Chrome 39, node.js 5.4):
* Operating System and version (desktop or mobile):
* Link to your project:
','Issue', 3, 'My code runs equally well on the server, as well as the browser')
) 
AS Source (ID, Content, Type, CategoryId, Description) 
ON Target.ID = Source.ID 
WHEN NOT MATCHED BY TARGET THEN 
INSERT (Content, Type, CategoryId, Description) 
VALUES (Content, Type, CategoryId, Description);

GO

MERGE INTO Templates AS Target 
USING (VALUES 
       (10,'<!--- Provide a general summary of your changes in the Title above -->

## Description
<!--- Describe your changes in detail -->

## Motivation and Context
<!--- Why is this change required? What problem does it solve? -->
<!--- If it fixes an open issue, please link to the issue here. -->

## How Has This Been Tested?
<!--- Please describe in detail how you tested your changes. -->
<!--- Include details of your testing environment, and the tests you ran to -->
<!--- see how your change affects other areas of the code, etc. -->

## Screenshots (if appropriate):

## Types of changes
<!--- What types of changes does your code introduce? Put an `x` in all the boxes that apply: -->
- [ ] Bug fix (non-breaking change which fixes an issue)
- [ ] New feature (non-breaking change which adds functionality)
- [ ] Breaking change (fix or feature that would cause existing functionality to change)

## Checklist:
<!--- Go over all the following points, and put an `x` in all the boxes that apply. -->
<!--- If you unsure about any of these, do not hesitate to ask. We are here to help! -->
- [ ] My code follows the code style of this project.
- [ ] My change requires a change to the documentation.
- [ ] I have updated the documentation accordingly.
- [ ] I have read the **CONTRIBUTING** document.
- [ ] I have added tests to cover my changes.
- [ ] All new and existing tests passed.
','PullRequest', 'We accept pull requests even without issues'),
(11,'<!--- Provide a general summary of your changes in the Title above -->

## Description
<!--- Describe your changes in detail -->

## Related Issue
<!--- This project only accepts pull requests related to open issues -->
<!--- If suggesting a new feature or change, please discuss it in an issue first -->
<!--- If fixing a bug, there should be an issue describing it with steps to reproduce -->
<!--- Please link to the issue here: -->

## Motivation and Context
<!--- Why is this change required? What problem does it solve? -->

## How Has This Been Tested?
<!--- Please describe in detail how you tested your changes. -->
<!--- Include details of your testing environment, and the tests you ran to -->
<!--- see how your change affects other areas of the code, etc. -->

## Screenshots (if appropriate):

## Types of changes
<!--- What types of changes does your code introduce? Put an `x` in all the boxes that apply: -->
- [ ] Bug fix (non-breaking change which fixes an issue)
- [ ] New feature (non-breaking change which adds functionality)
- [ ] Breaking change (fix or feature that would cause existing functionality to change)

## Checklist:
<!--- Go over all the following points, and put an `x` in all the boxes that apply. -->
<!--- If you are unsure about any of these, do not hesitate to ask. We are here to help! -->
- [ ] My code follows the code style of this project.
- [ ] My change requires a change to the documentation.
- [ ] I have updated the documentation accordingly.
- [ ] I have read the **CONTRIBUTING** document.
- [ ] I have added tests to cover my changes.
- [ ] All new and existing tests passed.
','PullRequest', 'We also only accept pull requests after the matter has been discussed in an issue')
) 
AS Source (ID, Content, Type, Description) 
ON Target.ID = Source.ID 
WHEN NOT MATCHED BY TARGET THEN 
INSERT (Content, Type, Description) 
VALUES (Content, Type, Description);

GO

